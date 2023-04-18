using DMSpro.OMS.MdmService.MCPDetails;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.MCPHeaders;
using Volo.Abp;
using DMSpro.OMS.MdmService.Customers;
using System.Globalization;
using Volo.Abp.Guids;
using System.Linq;
using DMSpro.OMS.MdmService.HolidayDetails;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.CompanyInZones;
using DMSpro.OMS.MdmService.Localization;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlansScheduledAppService : ApplicationService, IVisitPlansScheduledAppService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRepository<Customer, Guid> _customerRepository;
        private readonly IRepository<SalesOrgHierarchy, Guid> _salesOrgHierarchyRepository;
        private readonly IRepository<Company, Guid> _companyRepository;
        private readonly IRepository<MCPHeader, Guid> _mcpHeaderRepository;
        private readonly ICompanyInZoneRepository _companyInZoneRepository;
        private readonly IMCPDetailCustomRepository _mcpDetailCustomRepository;
        private readonly IVisitPlanRepository _visitPlanRepository;
        private readonly IMCPHeaderCustomRepository _mCPHeaderCustomRepository;
        private readonly IHolidayDetailRepository _holidayDetailRepository;

        public VisitPlansScheduledAppService(
            IGuidGenerator guidGenerator,
            IRepository<SalesOrgHierarchy, Guid> salesOrgHierarchyRepository,
            IRepository<Company, Guid> companyRepository,
            ICompanyInZoneRepository companyInZoneRepository,
            IRepository<Customer, Guid> customerRepository,
            IRepository<MCPHeader, Guid> mcpHeaderRepository,
            IMCPDetailCustomRepository mcpDetailCustomRepository,
            IVisitPlanRepository visitPlanRepository,
            IMCPHeaderCustomRepository mCPHeaderCustomRepository,
            IHolidayDetailRepository holidayDetailRepository
        )
        {
            _visitPlanRepository = visitPlanRepository;
            _guidGenerator = guidGenerator;
            _customerRepository = customerRepository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _companyInZoneRepository = companyInZoneRepository;
            _companyRepository = companyRepository;
            _mcpHeaderRepository = mcpHeaderRepository;
            _mcpDetailCustomRepository = mcpDetailCustomRepository;
            _mCPHeaderCustomRepository = mCPHeaderCustomRepository;
            _holidayDetailRepository = holidayDetailRepository;

            LocalizationResource = typeof(MdmServiceResource);
        }

        public async Task SheduledGenerationAsync()
        {
            List<Guid> eligibleMCPHeaderIds = await _mCPHeaderCustomRepository.GetMCPHeaderIdForScheduledVisitPlanGeneration();
            foreach (Guid mcpHeaderId in eligibleMCPHeaderIds)
            {
                VisitPlanGenerationInputDto visitPlanGenerationInputDto = new() { MCPHeaderId = mcpHeaderId, };
                await GenerateAsync(visitPlanGenerationInputDto);
            }
        }

        public async Task<List<VisitPlanDto>> GenerateAsync(VisitPlanGenerationInputDto input)
        {
            DateTime reference = DateTime.Now;
            MCPHeader mcpHeader = await GetMCPHeader(input.MCPHeaderId, reference);
            SalesOrgHierarchy route = await GetRoute(mcpHeader.RouteId);
            SalesOrgHierarchy sellingZone = await GetSellingZone(route);
            CompanyInZone companyInZone = await GetCompanyInZone(sellingZone.Id, mcpHeader.CompanyId, reference);
            await CheckCompany(mcpHeader.CompanyId);
            Tuple<DateTime, DateTime> processedInputDates = ProcessInputDates(reference, input.DateStart, input.DateEnd);
            DateTime DateStart = GetMaxDateFromList(processedInputDates.Item1, mcpHeader.EffectiveDate, companyInZone.EffectiveDate.Date).Date;
            DateTime DateEnd = ((DateTime)GetMinDateFromList(processedInputDates.Item2, mcpHeader.EndDate, companyInZone.EndDate)).Date;
            List<DateTime> holidays = await GetHolidayDates(DateStart, DateEnd);
            List<Tuple<DateTime, int, DayOfWeek>> DateDetails = GetDateDetails(DateStart, DateEnd, holidays);
            List<MCPDetail> MCPDetails = await GetMCPDetails(input.MCPHeaderId, input.MCPDetailIds);
            List<Guid> mcpDetailIds = MCPDetails.Select(c => c.Id).ToList();
            await DeleteExistingVisitPlans(DateStart, DateEnd, mcpDetailIds);
            List<VisitPlan> allVisitPlans = new();
            int successfulGeneration = 0;
            foreach (MCPDetail mCPDetail in MCPDetails)
            {
                try
                {
                    List<VisitPlan> visitPlans = await GenerateVisitPlanForMCPDetail(mCPDetail, DateStart, DateEnd, DateDetails,
                        route.Id, mcpHeader.ItemGroupId);
                    allVisitPlans.AddRange(visitPlans);
                    successfulGeneration++;
                    Console.WriteLine($"{visitPlans.Count} visit plans will be generated for MCPDetail ${mCPDetail.Code}.");
                }
                catch (UserFriendlyException ufe)
                {
                    Console.WriteLine($"Failed to generate visit plan for MCPDetail {mCPDetail.Code}. Error: {ufe.Message}.");
                }
            }
            await _visitPlanRepository.InsertManyAsync(allVisitPlans);

            Console.WriteLine($"{allVisitPlans.Count} visit plans were generated for {successfulGeneration}/{mcpDetailIds.Count} MCPDetail.");
            return ObjectMapper.Map<List<VisitPlan>, List<VisitPlanDto>>(allVisitPlans);
        }

        private async Task<List<VisitPlan>> GenerateVisitPlanForMCPDetail(MCPDetail mcpDetail, DateTime inputDateStart, DateTime inputDateEnd,
            List<Tuple<DateTime, int, DayOfWeek>> dateDetails, Guid routeId, Guid? itemGroupId)
        {
            List<VisitPlan> result = new();
            Customer customer = await _customerRepository.GetAsync(mcpDetail.CustomerId);
            if (!customer.Active)
            {
                return result;
            }
            DateTime MCPDetailDateStart = mcpDetail.EffectiveDate.Date;
            DateTime? MCPDetailDateEnd = mcpDetail.EndDate;
            if (MCPDetailDateEnd != null && MCPDetailDateEnd < MCPDetailDateStart)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:560"], code: "0");
            }
            DateTime dateStart = GetMaxDateFromList(inputDateStart, MCPDetailDateStart);
            DateTime dateEnd = ((DateTime)GetMinDateFromList(inputDateEnd, MCPDetailDateEnd)).Date;
            foreach (var dateDetail in dateDetails)
            {
                DateTime date = dateDetail.Item1;
                if (date < dateStart)
                {
                    continue;
                }
                if (date > dateEnd)
                {
                    break;
                }
                int WeekNum = dateDetail.Item2;
                DayOfWeek dayOfWeek = dateDetail.Item3;
                List<DayOfWeek> DoWs = GetMCPDayOfWeek(mcpDetail);
                List<int> Weeks = GetMCPWeek(mcpDetail);
                if (!Weeks.Contains(WeekNum) || !DoWs.Contains(dayOfWeek))
                {
                    continue;
                }
                bool isCommando = false;
                int weekInYear = ISOWeek.GetWeekOfYear(date);
                VisitPlan visitPlan =
                    new(_guidGenerator.Create(), mcpDetail.Id, customer.Id, routeId, itemGroupId,
                        date, mcpDetail.Distance, mcpDetail.VisitOrder,
                        dayOfWeek, weekInYear, date.Month, date.Year, isCommando);
                result.Add(visitPlan);
            }
            return result;
        }
        private static List<DayOfWeek> GetMCPDayOfWeek(MCPDetail mcpDetail)
        {
            List<DayOfWeek> DoWs = new();
            if (mcpDetail.Monday == true)
            {
                DoWs.Add(DayOfWeek.Monday);
            }
            if (mcpDetail.Tuesday == true)
            {
                DoWs.Add(DayOfWeek.Tuesday);
            }
            if (mcpDetail.Wednesday == true)
            {
                DoWs.Add(DayOfWeek.Wednesday);
            }
            if (mcpDetail.Thursday == true)
            {
                DoWs.Add(DayOfWeek.Thursday);
            }
            if (mcpDetail.Friday == true)
            {
                DoWs.Add(DayOfWeek.Friday);
            }
            if (mcpDetail.Saturday == true)
            {
                DoWs.Add(DayOfWeek.Saturday);
            }
            if (mcpDetail.Sunday == true)
            {
                DoWs.Add(DayOfWeek.Sunday);
            }
            return DoWs;
        }

        private static List<int> GetMCPWeek(MCPDetail mcpDetail)
        {
            List<int> mcpWeeks = new();
            if (mcpDetail.Week1 == true)
            {
                mcpWeeks.Add(1);
            }
            if (mcpDetail.Week2 == true)
            {
                mcpWeeks.Add(2);
            }
            if (mcpDetail.Week3 == true)
            {
                mcpWeeks.Add(3);
            }
            if (mcpDetail.Week4 == true)
            {
                mcpWeeks.Add(4);
            }
            return mcpWeeks;
        }

        private static List<Tuple<DateTime, int, DayOfWeek>> GetDateDetails(DateTime dateStart, DateTime dateEnd, List<DateTime> holidays)
        {
            List<Tuple<DateTime, int, DayOfWeek>> result = new();
            DateTime dateOver = dateEnd.Date.AddDays(1);
            for (int i = 0; ; i++)
            {
                DateTime date = dateStart.Date.AddDays(i);
                if (holidays.Contains(date))
                {
                    continue;
                }
                if (date >= dateOver)
                {
                    break;
                }
                int WeekNum = ISOWeek.GetWeekOfYear(date) % 4;
                if (WeekNum == 0)
                {
                    WeekNum = 4;
                }
                DayOfWeek mdmDoW = (DayOfWeek)date.DayOfWeek;
                result.Add(new Tuple<DateTime, int, DayOfWeek>(date, WeekNum, mdmDoW));
            }
            return result;
        }

        private async Task<List<MCPDetail>> GetMCPDetails(Guid mcpHeaderId, List<Guid> mcpDetailIds)
        {
            if (mcpDetailIds == null || mcpDetailIds.Count < 1)
            {
                return await _mcpDetailCustomRepository.GetListWithMCPHeaderAsync(mcpHeaderId);

            }
            return await _mcpDetailCustomRepository.GetListWithIdsAsync(mcpHeaderId, mcpDetailIds);
        }

        private static Tuple<DateTime, DateTime> ProcessInputDates(DateTime reference, DateTime? inputDateStart, DateTime? inputDateEnd)
        {
            DateTime DateStart = CheckInputDateStart(reference, inputDateStart);
            DateTime DateEnd = CheckInputDateEnd(reference, inputDateEnd, DateStart);
            return new Tuple<DateTime, DateTime>(DateStart, DateEnd);
        }

        private static DateTime CheckInputDateStart(DateTime reference, DateTime? inputDateStart)
        {
            DateTime Today = reference.Date;
            DateTime? DateStart = inputDateStart;
            if (DateStart == null || DateStart <= Today)
            {
                DateStart = Today.AddDays(1);
            }

            return ((DateTime)DateStart).Date;
        }

        private static DateTime CheckInputDateEnd(DateTime reference, DateTime? inputDateEnd, DateTime DateStart)
        {
            DateTime? DateEnd = inputDateEnd;

            // if there is no end date input, the end date will be:
            // until the end of the current month plus next 2 months
            // For example: current date is 17/12/2022, then end date would be 28/02/2023
            if (DateEnd == null)
            {
                DateTime firstDayThisMonth = new(reference.Year, reference.Month, 1);
                DateTime firstDayPlusThreeMonths = firstDayThisMonth.AddMonths(3);
                DateTime lastDayNextTwoMonth = firstDayPlusThreeMonths.AddDays(-1);
                DateEnd = lastDayNextTwoMonth.Date;
            }

            if (DateEnd < DateStart)
            {
                DateEnd = DateStart;
            }

            return ((DateTime)DateEnd).Date;
        }

        private static DateTime GetMaxDateFromList(params DateTime[] dates)
        {
            return dates.ToList().AsQueryable().Max();
        }

        private static DateTime? GetMinDateFromList(params DateTime?[] dates)
        {
            return dates.ToList().AsQueryable().Min();
        }

        private async Task<MCPHeader> GetMCPHeader(Guid mcpHeaderId, DateTime now)
        {
            var mcpHeader = await _mcpHeaderRepository.GetAsync(mcpHeaderId);
            if (mcpHeader.EffectiveDate < now && (mcpHeader.EndDate == null || mcpHeader.EndDate > now))
            {
                return mcpHeader;
            }
            throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:556"], code: "0");
        }

        private async Task<SalesOrgHierarchy> GetRoute(Guid routeId)
        {
            var route = await _salesOrgHierarchyRepository.GetAsync(routeId);
            if (!route.IsRoute)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:557"], code: "1");
            }
            if (!route.Active)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:557"], code: "0");
            }
            return route;
        }

        private async Task<SalesOrgHierarchy> GetSellingZone(SalesOrgHierarchy route)
        {
            SalesOrgHierarchy sellingZone = await _salesOrgHierarchyRepository.GetAsync(
                x => x.Id == route.ParentId);
            if (!sellingZone.IsSellingZone)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:550"], code: "1");
            }
            if (!sellingZone.Active)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:551"], code: "1");
            }
            return sellingZone;
        }

        private async Task<CompanyInZone> GetCompanyInZone(Guid sellingZoneId, Guid companyId, DateTime now)
        {
            var assignments = await _companyInZoneRepository.GetListAsync(
                x => x.SalesOrgHierarchyId == sellingZoneId && x.CompanyId == companyId &&
                x.EffectiveDate < now && (x.EndDate == null || x.EndDate > now));

            if (assignments.Count < 1)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:552"], code: "0");
            }
            if (assignments.Count > 1)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:553"], code: "0");
            }
            return assignments.First();
        }

        private async Task CheckCompany(Guid companyId)
        {
            var companies = await _companyRepository.GetListAsync(x => x.Id == companyId &&
                x.Active == true);
            if (!companies.Any())
            {
                throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:554"], code: "0");
            }
        }

        private async Task DeleteExistingVisitPlans(DateTime DateStart, DateTime DateEnd, List<Guid> mcpDetailIds)
        {
            await _visitPlanRepository.DeleteExistingVisitPlansAsync(DateStart, DateEnd, mcpDetailIds);
        }

        private async Task<List<DateTime>> GetHolidayDates(DateTime dateStart, DateTime dateEnd)
        {
            List<HolidayDetail> holidays = await GetHolidayDetailsWithinRange(dateStart, dateEnd);
            List<DateTime> result = new();
            foreach (HolidayDetail holiday in holidays)
            {
                for (DateTime dt = holiday.StartDate.Date; dt <= holiday.EndDate.Date; dt = dt.AddDays(1))
                {
                    if (!result.Contains(dt))
                    {
                        result.Add(dt);
                    }
                }
            }
            return result;
        }

        private async Task<List<HolidayDetail>> GetHolidayDetailsWithinRange(DateTime dateStart, DateTime dateEnd)
        {
            DateTime dateEndMax = dateEnd.Date.AddDays(1).AddSeconds(-1);
            return await _holidayDetailRepository.GetListAsync(
                b => (b.StartDate.Date <= dateStart.Date && b.EndDate.Date >= dateEndMax) ||
                (b.StartDate.Date >= dateStart.Date && b.StartDate.Date <= dateEndMax) ||
                (b.EndDate.Date >= dateStart.Date && b.EndDate.Date <= dateEndMax));
        }
    }
}
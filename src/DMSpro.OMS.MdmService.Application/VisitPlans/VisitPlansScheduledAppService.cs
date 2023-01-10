using DMSpro.OMS.MdmService.MCPDetails;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.MCPHeaders;
using Volo.Abp;
using Microsoft.Extensions.Logging;
using DMSpro.OMS.MdmService.Customers;
using System.Globalization;
using Volo.Abp.Guids;
using System.Linq;
using DMSpro.OMS.MdmService.HolidayDetails;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.CompanyInZones;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlansScheduledAppService : ApplicationService, IVisitPlansScheduledAppService
    {
        private readonly IVisitPlanCustomRepository _visitPlanCustomRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRepository<Customer, Guid> _customerRepository;
        private readonly IRepository<SalesOrgHierarchy, Guid> _salesOrgHierarchyRepository;
        private readonly IRepository<Company, Guid> _companyRepository;
        private readonly IRepository<MCPHeader, Guid> _mcpHeaderRepository;
        private readonly ICompanyInZoneRepository _companyInZoneRepository;
        private readonly IMCPDetailCustomRepository _mcpDetailCustomRepository;
        private readonly IVisitPlanRepository _visitPlanRepository;
        private readonly IMCPHeaderCustomRepository _mCPHeaderCustomRepository;
        private readonly IHolidayDetailCustomRepository _holidayDetailCustomRepository;

        public VisitPlansScheduledAppService(IVisitPlanCustomRepository visitPlanCustomRepository,
            IGuidGenerator guidGenerator,
            IRepository<SalesOrgHierarchy, Guid> salesOrgHierarchyRepository,
            IRepository<Company, Guid> companyRepository,
            ICompanyInZoneRepository companyInZoneRepository,
            IRepository<Customer, Guid> customerRepository,
            IRepository<MCPHeader, Guid> mcpHeaderRepository,
            IMCPDetailCustomRepository mcpDetailCustomRepository,
            IVisitPlanRepository visitPlanRepository,
            IMCPHeaderCustomRepository mCPHeaderCustomRepository,
            IHolidayDetailCustomRepository holidayDetailCustomRepository
        )
        {
            _visitPlanRepository = visitPlanRepository;
            _visitPlanCustomRepository = visitPlanCustomRepository;
            _guidGenerator = guidGenerator;
            _customerRepository = customerRepository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _companyInZoneRepository = companyInZoneRepository;
            _companyRepository = companyRepository;
            _mcpHeaderRepository = mcpHeaderRepository;
            _mcpDetailCustomRepository = mcpDetailCustomRepository;
            _mCPHeaderCustomRepository = mCPHeaderCustomRepository;
            _holidayDetailCustomRepository = holidayDetailCustomRepository;
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
            MCPHeader mcpHeader = await GetMCPHeaderData(input.MCPHeaderId);
            CompanyInZone companyInZone = await CheckCompanyInZone(mcpHeader.RouteId, mcpHeader.CompanyId);
            SalesOrgHierarchy route = await CheckRoute(mcpHeader.RouteId);
            Tuple<DateTime, DateTime?> companyData = await GetCompanyData(mcpHeader.CompanyId);
            Tuple<DateTime, DateTime> processedInputDates = ProcessInputDates(reference, input.DateStart, input.DateEnd);
            DateTime DateStart = GetMaxDateFromList(processedInputDates.Item1, mcpHeader.EffectiveDate, companyData.Item1, companyInZone.EffectiveDate.Date).Date;
            DateTime DateEnd = ((DateTime)GetMinDateFromList(processedInputDates.Item2, mcpHeader.EndDate, companyData.Item2, companyInZone.EndDate)).Date;
            List<DateTime> holidays = await GetHolidayDates(DateStart, DateEnd);
            List<Tuple<DateTime, int, DayOfWeek>> DateDetails = GetDateDetails(DateStart, DateEnd, holidays);
            List<MCPDetail> MCPDetails = await GetMCPDetails(input.MCPHeaderId, input.MCPDetailIds);
            List<Guid> mcpDetailIds = MCPDetails.AsQueryable().Select(c => c.Id).ToList();
            await DeleteExistingVisitPlans(DateStart, DateEnd, mcpDetailIds);
            List<VisitPlan> allVisitPlans = new();
            int successfulGeneration = 0;
            foreach (MCPDetail mCPDetail in MCPDetails)
            {
                try
                {
                    List<VisitPlan> visitPlans = await GenerateVisitPlanForMCPDetail(mCPDetail, DateStart, DateEnd, DateDetails,
                        route.Id, mcpHeader.CompanyId, mcpHeader.ItemGroupId);
                    allVisitPlans.AddRange(visitPlans);
                    successfulGeneration++;
                    Console.WriteLine($"{visitPlans.Count} visit plans will be generated for MCPDetail ${mCPDetail.Code}.");
                }
                catch (BusinessException be)
                {
                    Console.WriteLine($"Failed to generate visit plan for MCPDetail {mCPDetail.Code}. Error: {be.Message}.");
                }
            }
            await _visitPlanRepository.InsertManyAsync(allVisitPlans);

            Console.WriteLine($"{allVisitPlans.Count} visit plans were generated for {successfulGeneration}/{mcpDetailIds.Count} MCPDetail.");
            return ObjectMapper.Map<List<VisitPlan>, List<VisitPlanDto>>(allVisitPlans);
        }

        private async Task<List<VisitPlan>> GenerateVisitPlanForMCPDetail(MCPDetail mcpDetail, DateTime inputDateStart, DateTime inputDateEnd,
            List<Tuple<DateTime, int, DayOfWeek>> dateDetails, Guid routeId, Guid companyId, Guid? itemGroupId)
        {
            List<VisitPlan> result = new();
            Customer customer = await _customerRepository.GetAsync(mcpDetail.CustomerId);
            if (!customer.Active)
            {
                return result;
            }
            DateTime CustomerDateStart = customer.EffectiveDate.Date;
            DateTime? CustomerDateEnd = customer.EndDate;
            if (CustomerDateEnd != null && CustomerDateEnd < CustomerDateStart)
            {
                throw new BusinessException("707", "Bad Customer Data", "Customer end date cannot be smaller or equal to effective date", null, LogLevel.Critical);
            }
            DateTime MCPDetailDateStart = mcpDetail.EffectiveDate.Date;
            DateTime? MCPDetailDateEnd = mcpDetail.EndDate;
            if (MCPDetailDateEnd != null && MCPDetailDateEnd < MCPDetailDateStart)
            {
                throw new BusinessException("708", "Bad MCPDetail Data", "MCPDetail end date cannot be smaller or equal to effective date", null, LogLevel.Critical);
            }
            DateTime dateStart = GetMaxDateFromList(inputDateStart, CustomerDateStart, MCPDetailDateStart);
            DateTime dateEnd = ((DateTime)GetMinDateFromList(inputDateEnd, CustomerDateEnd, MCPDetailDateEnd)).Date;
            foreach (Tuple<DateTime, int, DayOfWeek> dateDetail in dateDetails)
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
                VisitPlan visitPlan =
                    new(_guidGenerator.Create(), mcpDetail.Id, customer.Id, routeId, companyId, itemGroupId, 
                        date, mcpDetail.Distance, mcpDetail.VisitOrder, dayOfWeek, WeekNum, date.Month, date.Year)
                    { TenantId = mcpDetail.TenantId };
                result.Add(visitPlan);
            }
            return result;
        }
        private static List<DayOfWeek> GetMCPDayOfWeek(MCPDetail mcpDetail)
        {
            List<DayOfWeek> DoWs = new();
            if (mcpDetail.Monday == true)
            {
                DoWs.Add(DayOfWeek.MONDAY);
            }
            if (mcpDetail.Tuesday == true)
            {
                DoWs.Add(DayOfWeek.TUESDAY);
            }
            if (mcpDetail.Wednesday == true)
            {
                DoWs.Add(DayOfWeek.WEDNESDAY);
            }
            if (mcpDetail.Thursday == true)
            {
                DoWs.Add(DayOfWeek.THURSDAY);
            }
            if (mcpDetail.Friday == true)
            {
                DoWs.Add(DayOfWeek.FRIDAY);
            }
            if (mcpDetail.Saturday == true)
            {
                DoWs.Add(DayOfWeek.SATURDAY);
            }
            if (mcpDetail.Sunday == true)
            {
                DoWs.Add(DayOfWeek.SUNDAY);
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
                DateTime firstDayThisMonth = new DateTime(reference.Year, reference.Month, 1);
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

        private async Task<MCPHeader> GetMCPHeaderData(Guid mcpHeaderId)
        {
            MCPHeader mcpHeader = await _mcpHeaderRepository.GetAsync(mcpHeaderId);
            if (mcpHeader.EndDate != null && mcpHeader.EndDate < mcpHeader.EffectiveDate)
            {
                throw new BusinessException("700", "Bad MCP Header Data", "MCPHeader end date cannot be smaller or equal to effective date", null, LogLevel.Critical);
            }
            return mcpHeader;
        }

        private async Task<SalesOrgHierarchy> CheckRoute(Guid routeId)
        {
            SalesOrgHierarchy route = await _salesOrgHierarchyRepository.GetAsync(routeId);
            if (!route.IsRoute)
            {
                throw new BusinessException("701", "Bad MCP Header Data", "RouteId does not represent a route", null, LogLevel.Critical);
            }
            if (!route.Active)
            {
                throw new BusinessException("702", "Bad MCP Header Data", "Route is no longer active", null, LogLevel.Critical);
            }
            return route;
        }

        private async Task<CompanyInZone> CheckCompanyInZone(Guid routeId, Guid companyId)
        {
            IQueryable<CompanyInZone> queryable = await _companyInZoneRepository.GetQueryableAsync();
            var query = from companyInZone in queryable
                        where companyInZone.SalesOrgHierarchyId == routeId &&
                            companyInZone.CompanyId == companyId &&
                            companyInZone.IsBase == true 
                        select companyInZone;
            var companyInZones = query.ToList();
            if (companyInZones.Count < 1)
            {
                throw new BusinessException("703", "Bad MCP Header Data", "Company is not a base in the route", null, LogLevel.Critical);
            }
            if (companyInZones.Count > 1)
            {
                throw new BusinessException("704", "Bad MCP Header Data", "Multiple route with the same company assigned and based", null, LogLevel.Critical);
            }
            CompanyInZone result = companyInZones.First();
            return result;
        }

        private async Task<Tuple<DateTime, DateTime?>> GetCompanyData(Guid companyId)
        {
            Company company = await _companyRepository.GetAsync(companyId);
            if (!company.Active)
            {
                throw new BusinessException("705", "Bad MCP Header Data", "Company is not active", null, LogLevel.Critical);
            }
            if (company.EndDate != null && company.EndDate < company.EffectiveDate.Date)
            {
                throw new BusinessException("706", "Bad Customer Data", "Company end date cannot be smaller or equal to effective date", null, LogLevel.Critical);
            }
            return new Tuple<DateTime, DateTime?>(company.EffectiveDate.Date, company.EndDate);
        }

        private async Task DeleteExistingVisitPlans(DateTime DateStart, DateTime DateEnd, List<Guid> mcpDetailIds)
        {
            await _visitPlanCustomRepository.DeleteExistingVisitPlansAsync(DateStart, DateEnd, mcpDetailIds);
        }

        private async Task<List<DateTime>> GetHolidayDates(DateTime dateStart, DateTime dateEnd)
        {
            DateTime dateStartMax = dateStart.Date.AddDays(1).AddSeconds(-1);
            DateTime dateEndMax = dateEnd.Date.AddDays(1).AddSeconds(-1);
            List<HolidayDetail> holidays =
                await _holidayDetailCustomRepository.GetHolidayDetailsWithinRange(dateStart, dateEnd);
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
    }
}
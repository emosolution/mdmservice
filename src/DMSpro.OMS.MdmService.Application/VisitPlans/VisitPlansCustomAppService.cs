using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using System;
using Volo.Abp;
using System.Globalization;
using Volo.Abp.Data;
using System.Linq;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public partial class VisitPlansAppService
    {
        [Authorize(MdmServicePermissions.VisitPlans.GenerateVisitPlanFromMCP)]
        public async Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input)
        {
            return await _visitPlansScheduledAppService.GenerateAsync(input);
        }

        [Authorize(MdmServicePermissions.VisitPlans.Edit)]
        public virtual async Task<List<VisitPlanDto>> UpdateMultipleAsync(List<Guid> ids, DateTime newDate)
        {
            DateTime tomorrow = DateTime.Now.AddDays(1).Date;
            if (newDate < tomorrow)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlansAppService:550"], code: "0");
            }
            List<VisitPlan> visitPlans = await _visitPlanRepository.GetListAsync(x => ids.Contains(x.Id));
            if (visitPlans.Count != ids.Count)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlansAppService:551"], code: "0");
            }
            var mcpDetailList = visitPlans.Select(x => x.MCPDetailId).ToList();
            var customerList = visitPlans.Select(x => x.CustomerId).ToList();
            var routeList = visitPlans.Select(x => x.RouteId).ToList();
            var itemGroupList = visitPlans.Select(x => x.ItemGroupId).ToList();
            var exitingVisitPlans = await _visitPlanRepository.GetListAsync(x =>
                x.DateVisit.Date == newDate.Date && !ids.Contains(x.Id) &&
                mcpDetailList.Contains(x.MCPDetailId) &&
                customerList.Contains(x.CustomerId) &&
                routeList.Contains(x.RouteId) && 
                itemGroupList.Contains(x.ItemGroupId));
            foreach (VisitPlan visitPlan in visitPlans)
            {
                if (visitPlan.DateVisit.Date < tomorrow)
                {
                    throw new UserFriendlyException(message: L["Error:VisitPlansAppService:552"], code: "0");
                }
                if (exitingVisitPlans.Any(x => x.MCPDetailId == visitPlan.MCPDetailId &&
                    x.CustomerId == visitPlan.CustomerId &&
                    x.RouteId == visitPlan.RouteId &&
                    x.ItemGroupId == visitPlan.ItemGroupId))
                {
                    throw new UserFriendlyException(message: L["Error:VisitPlansAppService:555"], code: "0");
                }
                visitPlan.DateVisit = newDate.Date;
            }
            await _visitPlanRepository.UpdateManyAsync(visitPlans);
            return ObjectMapper.Map<List<VisitPlan>, List<VisitPlanDto>>(visitPlans);
        }

        [Authorize(MdmServicePermissions.VisitPlans.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var record = await _visitPlanRepository.GetAsync(id);
            CheckVisitDate(record.DateVisit);
            await _visitPlanRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.VisitPlans.Create)]
        public virtual async Task<VisitPlanDto> CreateAsync(VisitPlanCreateDto input)
        {
            CheckInput(input.MCPDetailId, input.CustomerId, input.RouteId, input.DateVisit);
            await CheckExistingVisitPlan(input.MCPDetailId, input.CustomerId,
                input.RouteId, input.DateVisit);
            bool isCommando = true;
            VisitPlan visitPlan = new(GuidGenerator.Create(),
                input.MCPDetailId, input.CustomerId, input.RouteId, input.ItemGroupId,
                input.DateVisit, input.Distance, input.VisitOrder,
                input.DateVisit.DayOfWeek, ISOWeek.GetWeekOfYear(input.DateVisit),
                input.DateVisit.Month, input.DateVisit.Year, isCommando);
            var record = await _visitPlanRepository.InsertAsync(visitPlan);
            return ObjectMapper.Map<VisitPlan, VisitPlanDto>(record);
        }

        [Authorize(MdmServicePermissions.VisitPlans.Edit)]
        public virtual async Task<VisitPlanDto> UpdateAsync(Guid id, VisitPlanUpdateDto input)
        {
            CheckInput(input.MCPDetailId, input.CustomerId, input.RouteId, input.DateVisit);
            var record = await CheckExistingVisitPlan(input.MCPDetailId, input.CustomerId,
                input.RouteId, input.DateVisit, id);
            CheckVisitDate(record.DateVisit);

            record.DateVisit = input.DateVisit;
            record.Distance = input.Distance;
            record.VisitOrder = input.VisitOrder;
            record.DayOfWeek = input.DateVisit.DayOfWeek;
            record.Week = ISOWeek.GetWeekOfYear(input.DateVisit);
            record.Month = input.DateVisit.Month;
            record.Year = input.DateVisit.Year;
            record.IsCommando = true;
            record.CustomerId = input.CustomerId;
            record.RouteId = input.RouteId;
            record.ItemGroupId = input.ItemGroupId;
            record.MCPDetailId = input.MCPDetailId;
            record.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

            return ObjectMapper.Map<VisitPlan, VisitPlanDto>(record);
        }

        public virtual async Task<VisitPlanDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<VisitPlan, VisitPlanDto>(await _visitPlanRepository.GetAsync(id));
        }

        private void CheckVisitDate(DateTime date)
        {
            DateTime tomorrow = DateTime.Now.AddDays(1).Date;
            if (date < tomorrow)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlansAppService:550"], code: "0");
            }
        }

        private void CheckInput(Guid mcpDetailId, Guid customerId, Guid routeId, DateTime dateVisit)
        {
            CheckVisitDate(dateVisit);
            if (mcpDetailId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["MCPDetail"]]);
            }
            if (customerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            if (routeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            Check.NotNull(mcpDetailId, nameof(mcpDetailId));
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(dateVisit, nameof(dateVisit));
        }

        private async Task<VisitPlan> CheckExistingVisitPlan(Guid mCPDetailId, Guid customerId, Guid routeId,
            DateTime dateVisit, Guid? id = null)
        {
            var records = await _visitPlanRepository.GetListAsync(x => x.MCPDetailId == mCPDetailId &&
                x.CustomerId == customerId && x.RouteId == routeId && x.DateVisit.Date == dateVisit.Date);
            if (records.Count < 1)
            {
                return null;
            }
            if (records.Count > 1)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:561"], code: "0");
            }
            var record = records.First();
            if (id != null && record.Id == id)
            {
                return record;
            }
            throw new UserFriendlyException(message: L["Error:VisitPlanGeneration:559"], code: "0");
        }
    }
}
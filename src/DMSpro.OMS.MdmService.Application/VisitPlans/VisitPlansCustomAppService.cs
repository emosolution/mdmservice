using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using System;
using Volo.Abp;
using System.Globalization;

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
            CheckVisitDate(newDate);
            List<VisitPlan> visitPlans = await _visitPlanRepository.GetByIdAsync(ids);
            if (visitPlans.Count != ids.Count)
            {
                throw new BusinessException(message: L["Error:VisitPlansAppService:551"], code: "0");
            }
            DateTime tomorrow = DateTime.Now.AddDays(1).Date;
            foreach (VisitPlan visitPlan in visitPlans)
            {
                if (visitPlan.DateVisit.Date < tomorrow)
                {
                    throw new BusinessException(message: L["Error:VisitPlansAppService:552"], code: "0");
                }
                visitPlan.DateVisit = newDate.Date;
            }
            await _visitPlanRepository.UpdateManyAsync(visitPlans);
            return ObjectMapper.Map<List<VisitPlan>, List<VisitPlanDto>>(visitPlans);
        }

        [Authorize(MdmServicePermissions.VisitPlans.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _visitPlanRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.VisitPlans.Create)]
        public virtual async Task<VisitPlanDto> CreateAsync(VisitPlanCreateDto input)
        {
            CheckVisitDate(input.DateVisit);
            if (input.MCPDetailId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["MCPDetail"]]);
            }
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            if (input.RouteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            bool isCommando = true;
            var visitPlan = await _visitPlanManager.CreateAsync(
                input.MCPDetailId, input.CustomerId, input.RouteId, input.ItemGroupId,
                input.DateVisit, input.Distance, input.VisitOrder,
                (DayOfWeek)input.DateVisit.DayOfWeek, ISOWeek.GetWeekOfYear(input.DateVisit),
                input.DateVisit.Month, input.DateVisit.Year, isCommando);

            return ObjectMapper.Map<VisitPlan, VisitPlanDto>(visitPlan);
        }

        [Authorize(MdmServicePermissions.VisitPlans.Edit)]
        public virtual async Task<VisitPlanDto> UpdateAsync(Guid id, VisitPlanUpdateDto input)
        {
            CheckVisitDate(input.DateVisit);
            if (input.MCPDetailId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["MCPDetail"]]);
            }
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            if (input.RouteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            bool isCommando = true;
            var visitPlan = await _visitPlanManager.UpdateAsync(
                id,
                input.MCPDetailId, input.CustomerId, input.RouteId, input.ItemGroupId,
                input.DateVisit, input.Distance, input.VisitOrder,
                (DayOfWeek)input.DateVisit.DayOfWeek, ISOWeek.GetWeekOfYear(input.DateVisit),
                input.DateVisit.Month, input.DateVisit.Year, isCommando, input.ConcurrencyStamp);

            return ObjectMapper.Map<VisitPlan, VisitPlanDto>(visitPlan);
        }

        private void CheckVisitDate(DateTime date)
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = now.AddDays(1).Date;
            if (date < tomorrow)
            {
                throw new UserFriendlyException(message: L["Error:VisitPlansAppService:550"], code: "0");
            }
        }
    }
}
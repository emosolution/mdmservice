using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using System;
using Volo.Abp;

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
            DateTime tomorrow = DateTime.Now.Date.AddDays(1);
            if (newDate < tomorrow)
            {
                throw new BusinessException(message: L["Error:VisitPlansAppService:550"], code: "0");
            }
            List<VisitPlan> visitPlans = await _visitPlanRepository.GetByIdAsync(ids);
            if (visitPlans.Count != ids.Count)
            {
                throw new BusinessException(message: L["Error:VisitPlansAppService:551"], code: "0");
            }
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
    }
}
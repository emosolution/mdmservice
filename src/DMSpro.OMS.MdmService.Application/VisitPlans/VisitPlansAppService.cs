using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.VisitPlans
{

    [Authorize(MdmServicePermissions.VisitPlans.Default)]
    public partial class VisitPlansAppService
    { 
        public virtual async Task<VisitPlanDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<VisitPlan, VisitPlanDto>(await _visitPlanRepository.GetAsync(id));
        }
    }
}
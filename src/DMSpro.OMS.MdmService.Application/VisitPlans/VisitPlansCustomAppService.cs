using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public partial class VisitPlansAppService
    {
        [Authorize(MdmServicePermissions.VisitPlans.GenerateVisitPlanFromMCP)]
        public async Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input)
        {
            return await _visitPlansScheduledAppService.GenerateAsync(input);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.VisitPlans;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Controllers.VisitPlans
{
    public partial class VisitPlanController
    {
        [HttpPost]
        [Route("generate-visit-plan-from-mcp")]
        public Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input)
        {
            return _visitPlansAppService.GenerateWithPermissionAsync(input);
        }
    }
}
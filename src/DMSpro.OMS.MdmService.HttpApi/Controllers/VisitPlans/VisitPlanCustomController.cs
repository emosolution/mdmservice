using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.VisitPlans;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Controllers.VisitPlans
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("VisitPlan")]
    [Route("api/mdm-service/visit-plans")]
    public class VisitPlanCustomController : AbpController, IVisitPlansCustomAppService
    {
        private readonly IVisitPlansCustomAppService _visitPlansCustomAppService;

        public VisitPlanCustomController(IVisitPlansCustomAppService visitPlansCustomAppService)
        {
            _visitPlansCustomAppService = visitPlansCustomAppService;
        }

        [HttpPost]
        [Route("generate-visit-plan-from-mcp")]
        public Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input)
        {
            return _visitPlansCustomAppService.GenerateWithPermissionAsync(input);
        }
    }
}
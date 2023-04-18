using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.VisitPlans;
using System.Collections.Generic;
using System;

namespace DMSpro.OMS.MdmService.Controllers.VisitPlans
{
    public partial class VisitPlanController
    {
        [HttpPost]
        [Route("generate-visit-plan-from-mcp")]
        public virtual Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input)
        {
            return _visitPlansAppService.GenerateWithPermissionAsync(input);
            
        }

        [HttpPut]
        [Route("multiple")]
        public virtual Task<List<VisitPlanDto>> UpdateMultipleAsync(List<Guid> ids, DateTime newDate)
        {
            return _visitPlansAppService.UpdateMultipleAsync(ids, newDate);
        }
    }
}
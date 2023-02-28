using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.VisitPlans;
using System.Collections.Generic;
using System;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.Controllers.VisitPlans
{
    public partial class VisitPlanController
    {
        [HttpPost]
        [Route("generate-visit-plan-from-mcp")]
        public async Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input)
        {
            try
            {
                return await _visitPlansAppService.GenerateWithPermissionAsync(input);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }

        [HttpPut]
        [Route("multiple")]
        public async Task<List<VisitPlanDto>> UpdateMultipleAsync(List<Guid> ids, DateTime newDate)
        {
            try
            {
                return await _visitPlansAppService.UpdateMultipleAsync(ids, newDate);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }
    }
}
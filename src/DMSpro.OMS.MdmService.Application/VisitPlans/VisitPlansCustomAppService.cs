using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlansCustomAppService : ApplicationService, IVisitPlansCustomAppService
    {
        private readonly IVisitPlansScheduledAppService _visitPlansScheduledAppService;

        public VisitPlansCustomAppService(IVisitPlansScheduledAppService visitPlansScheduledAppService)
        {
            _visitPlansScheduledAppService = visitPlansScheduledAppService;
        }

        [Authorize(MdmServicePermissions.VisitPlans.GenerateVisitPlanFromMCP)]
        public async Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input)
        {
            return await _visitPlansScheduledAppService.GenerateAsync(input);
        }
    }
}
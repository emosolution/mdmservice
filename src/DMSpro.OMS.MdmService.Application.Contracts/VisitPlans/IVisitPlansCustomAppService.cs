using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public interface IVisitPlansCustomAppService : IApplicationService
    {
        Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public interface IVisitPlansScheduledAppService : IApplicationService
    {
        Task SheduledGenerationAsync();
        Task<List<VisitPlanDto>> GenerateAsync(VisitPlanGenerationInputDto input);
    }
}
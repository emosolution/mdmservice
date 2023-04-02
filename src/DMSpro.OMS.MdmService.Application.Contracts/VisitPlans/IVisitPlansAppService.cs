using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public partial interface IVisitPlansAppService : IApplicationService
    {
        Task<VisitPlanDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<VisitPlanDto> CreateAsync(VisitPlanCreateDto input);
        Task<VisitPlanDto> UpdateAsync(Guid id, VisitPlanUpdateDto input);
    }
}
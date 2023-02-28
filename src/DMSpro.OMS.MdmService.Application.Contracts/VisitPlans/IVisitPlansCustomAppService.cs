using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using System.Collections.Generic;
using System;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public partial interface IVisitPlansAppService
    {
        Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input);

        Task<List<VisitPlanDto>> UpdateMultipleAsync(List<Guid> ids, DateTime newDate);
    }
}
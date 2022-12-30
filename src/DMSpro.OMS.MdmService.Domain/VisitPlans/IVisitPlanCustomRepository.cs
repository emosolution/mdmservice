using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public interface IVisitPlanCustomRepository : IRepository<VisitPlan, Guid>
    {
        Task DeleteExistingVisitPlansAsync(
            DateTime DateStart, DateTime DateEnd, List<Guid> mcpDetailIds,
            CancellationToken cancellationToken = default
        );
    }
}
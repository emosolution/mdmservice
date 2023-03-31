using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.VisitPlans
{
	public partial interface IVisitPlanRepository : IRepository<VisitPlan, Guid>
    {
		Task<List<VisitPlan>> GetByIdAsync(List<Guid> ids);

        Task DeleteExistingVisitPlansAsync(
            DateTime DateStart, DateTime DateEnd, List<Guid> mcpDetailIds,
            CancellationToken cancellationToken = default
        );
    }
}
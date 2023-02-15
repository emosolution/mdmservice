using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class EfCoreVisitPlanInternalRepository : EfCoreRepository<MdmServiceDbContext, VisitPlan, Guid>, IVisitPlanInternalRepository
    {
        public EfCoreVisitPlanInternalRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task DeleteExistingVisitPlansAsync(DateTime DateStart, DateTime DateEnd, List<Guid> mcpDetailIds, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();
            var existingVisitPlans = dbContext.VisitPlans.Where(b => mcpDetailIds.Contains(b.MCPDetailId) && b.DateVisit >= DateStart && b.DateVisit <= DateEnd).ToList();
            await this.HardDeleteAsync(existingVisitPlans, cancellationToken: cancellationToken);
        }
    }
}

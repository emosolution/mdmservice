using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.VisitPlans
{
	public partial class EfCoreVisitPlanRepository : EfCoreRepository<MdmServiceDbContext, VisitPlan, Guid>, IVisitPlanRepository
    {
        public EfCoreVisitPlanRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

		public virtual async Task<List<VisitPlan>> GetByIdAsync(List<Guid> ids)
        {
            var items = (await GetDbSetAsync()).Where(x => ids.Contains(x.Id));
            return await items.ToListAsync();
        }

        public async Task DeleteExistingVisitPlansAsync(DateTime DateStart, DateTime DateEnd, List<Guid> mcpDetailIds, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();
            var existingVisitPlans = dbContext.VisitPlans.Where(
                b => mcpDetailIds.Contains(b.MCPDetailId) && 
                b.DateVisit >= DateStart && b.DateVisit <= DateEnd).ToList();
            await this.HardDeleteAsync(existingVisitPlans, cancellationToken: cancellationToken);
        }
    }
}
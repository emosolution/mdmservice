using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class EFCoreMCPHeaderCustomRepository : EfCoreRepository<MdmServiceDbContext, MCPHeader, Guid>, IMCPHeaderCustomRepository
    {
        public EFCoreMCPHeaderCustomRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Guid>> GetMCPHeaderIdForScheduledVisitPlanGeneration()
        {
            DateTime tomorrow = DateTime.Now.Date.AddDays(1);
            var dbContext = await GetDbContextAsync();
            var effectiveMCPHeaders  = dbContext.MCPHeaders.Where(c => c.EffectiveDate >= tomorrow);
            return effectiveMCPHeaders.Select(c => c.Id).ToList();
        }
    }
}
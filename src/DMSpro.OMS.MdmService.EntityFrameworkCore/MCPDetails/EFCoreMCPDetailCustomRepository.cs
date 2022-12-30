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

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public class EfCoreMCPDetailCustomRepository : EfCoreRepository<MdmServiceDbContext, MCPDetail, Guid>, IMCPDetailCustomRepository
    {
        public EfCoreMCPDetailCustomRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<MCPDetail>> GetListWithIdsAsync(Guid mcpHeaderId, List<Guid> mcpDetailIds, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();
            return dbContext.MCPDetails.Where(c => c.MCPHeaderId == mcpHeaderId && mcpDetailIds.Contains(c.Id)).ToList();
        }

        public async Task<List<MCPDetail>> GetListWithMCPHeaderAsync(Guid mcpHeaderId, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();
            return dbContext.MCPDetails.Where(c => c.MCPHeaderId == mcpHeaderId).ToList();
        }
    }
}
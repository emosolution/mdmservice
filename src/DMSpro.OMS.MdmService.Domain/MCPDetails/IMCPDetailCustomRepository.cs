using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public interface IMCPDetailCustomRepository : IRepository<MCPDetail, Guid>
    {
        Task<List<MCPDetail>> GetListWithMCPHeaderAsync(
                    Guid mcpHeaderId,
                    CancellationToken cancellationToken = default
        );

        Task<List<MCPDetail>> GetListWithIdsAsync(
                    Guid mcpHeaderId,
                    List<Guid> mcpDetailIds,
                    CancellationToken cancellationToken = default
        );
    }
}
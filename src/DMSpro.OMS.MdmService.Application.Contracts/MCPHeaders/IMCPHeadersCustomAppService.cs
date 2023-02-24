using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public partial interface IMCPHeadersAppService
    {
        Task SetEndDateAsync(Guid id, DateTime endDate);

        Task<MCPDto> CreateMCPAsync(MCPCreateDto mcpCreateDto);

        Task<MCPDto> UpdateMCPAsync(Guid headerId, MCPUpdateDto mcpUpdateDto);
    }
}

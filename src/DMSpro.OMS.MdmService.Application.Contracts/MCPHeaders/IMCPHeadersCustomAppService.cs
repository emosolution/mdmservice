using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public partial interface IMCPHeadersAppService
    {
        Task SetEndDate(Guid id, DateTime endDate);

        Task<MCPDto> CreateMCP(MCPCreateDto mcpCreateDto);
    }
}

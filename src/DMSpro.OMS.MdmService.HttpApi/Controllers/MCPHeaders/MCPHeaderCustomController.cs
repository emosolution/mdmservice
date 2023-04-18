using DMSpro.OMS.MdmService.MCPHeaders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.MCPHeaders
{
    public partial class MCPHeaderController
    {
        [HttpPut]
        [Route("end-date")]
        public virtual Task SetEndDateAsync(Guid id, DateTime endDate)
        {
            return _mCPHeadersAppService.SetEndDateAsync(id, endDate);
        }

        [HttpPost]
        [Route("create-mcp")]
        public virtual Task<MCPDto> CreateMCPAsync(MCPCreateDto mcpCreateDto)
        {
            return _mCPHeadersAppService.CreateMCPAsync(mcpCreateDto);
        }


        [HttpPut]
        [Route("update-mcp/{headerId}")]
        public virtual Task<MCPDto> UpdateMCPAsync(Guid headerId, MCPUpdateDto mcpUpdateDto)
        {
            return _mCPHeadersAppService.UpdateMCPAsync(headerId, mcpUpdateDto);
        }
    }
}

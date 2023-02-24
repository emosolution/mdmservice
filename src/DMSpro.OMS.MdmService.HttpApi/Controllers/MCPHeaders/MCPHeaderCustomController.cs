using DMSpro.OMS.MdmService.MCPHeaders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.Controllers.MCPHeaders
{
    public partial class MCPHeaderController
    {
        [HttpPut]
        [Route("end-date")]
        public async Task SetEndDateAsync(Guid id, DateTime endDate)
        {
            try
            {
                await _mCPHeadersAppService.SetEndDateAsync(id, endDate);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }

        [HttpPost]
        [Route("create-mcp")]
        public async Task<MCPDto> CreateMCPAsync(MCPCreateDto mcpCreateDto)
        {
            try
            {
                return await _mCPHeadersAppService.CreateMCPAsync(mcpCreateDto);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }


        [HttpPut]
        [Route("update-mcp/{headerId}")]
        public async Task<MCPDto> UpdateMCPAsync(Guid headerId, MCPUpdateDto mcpUpdateDto)
        {
            try
            {
                return await _mCPHeadersAppService.UpdateMCPAsync(headerId, mcpUpdateDto);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }

    }
}

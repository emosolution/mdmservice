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
        public async Task SetEndDate(Guid id, DateTime endDate)
        {
            try
            {
                await _mCPHeadersAppService.SetEndDate(id, endDate);
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

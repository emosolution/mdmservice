using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using System;
using DMSpro.OMS.MdmService.PriceLists;

namespace DMSpro.OMS.MdmService.Controllers.PriceLists
{
    public partial class PriceListController
    {
        [HttpPut]
        [Route("release")]
        public virtual async Task<PriceListDto> ReleaseAsync(Guid id)
        {
            try
            {
                return await _priceListsAppService.ReleaseAsync(id);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }
    }
}
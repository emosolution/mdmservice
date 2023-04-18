using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using DMSpro.OMS.MdmService.PriceLists;

namespace DMSpro.OMS.MdmService.Controllers.PriceLists
{
    public partial class PriceListController
    {
        [HttpPut]
        [Route("release")]
        public virtual Task<PriceListDto> ReleaseAsync(Guid id)
        {
            return _priceListsAppService.ReleaseAsync(id);
        }
    }
}
using DMSpro.OMS.MdmService.Items;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.Items
{
    public partial class ItemController
    {
        [HttpGet]
        [Route("item-profile/{id}")]
        public virtual Task<ItemProfileDto> GetItemProfileAsync(Guid id)
        {
            return _itemsAppService.GetItemProfileAsync(id);
        }
    }
}

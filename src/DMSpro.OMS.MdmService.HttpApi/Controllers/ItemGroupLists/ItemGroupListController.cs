using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.ItemGroupLists;

namespace DMSpro.OMS.MdmService.Controllers.ItemGroupLists
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemGroupList")]
    [Route("api/mdm-service/item-group-lists")]
    public partial class ItemGroupListController : AbpController, IItemGroupListsAppService
    {
        private readonly IItemGroupListsAppService _itemGroupListsAppService;

        public ItemGroupListController(IItemGroupListsAppService itemGroupListsAppService)
        {
            _itemGroupListsAppService = itemGroupListsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemGroupListDto> GetAsync(Guid id)
        {
            return _itemGroupListsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ItemGroupListDto> CreateAsync(ItemGroupListCreateDto input)
        {
            return _itemGroupListsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemGroupListDto> UpdateAsync(Guid id, ItemGroupListUpdateDto input)
        {
            return _itemGroupListsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemGroupListsAppService.DeleteAsync(id);
        }
    }
}
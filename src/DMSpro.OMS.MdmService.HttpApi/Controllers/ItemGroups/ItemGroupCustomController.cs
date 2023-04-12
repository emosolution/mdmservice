using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.ItemGroups;

namespace DMSpro.OMS.MdmService.Controllers.ItemGroups
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemGroup")]
    [Route("api/mdm-service/item-groups")]
    public partial class ItemGroupController : AbpController, IItemGroupsAppService
    {
        private readonly IItemGroupsAppService _itemGroupsAppService;

        public ItemGroupController(IItemGroupsAppService itemGroupsAppService)
        {
            _itemGroupsAppService = itemGroupsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemGroupDto> GetAsync(Guid id)
        {
            return _itemGroupsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ItemGroupDto> CreateAsync(ItemGroupCreateDto input)
        {
            return _itemGroupsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemGroupDto> UpdateAsync(Guid id, ItemGroupUpdateDto input)
        {
            return _itemGroupsAppService.UpdateAsync(id, input);
        }

        [HttpPut]
        [Route("release/{id}")]
        public virtual Task<ItemGroupDto> ReleaseAsync(Guid id)
        {
            return _itemGroupsAppService.ReleaseAsync(id);
        }
    }
}
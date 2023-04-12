using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.ItemGroupAttributes
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemGroupAttribute")]
    [Route("api/mdm-service/item-group-attributes")]
    public partial class ItemGroupAttributeController : AbpController, IItemGroupAttributesAppService
    {
        private readonly IItemGroupAttributesAppService _itemGroupAttributesAppService;

        public ItemGroupAttributeController(IItemGroupAttributesAppService itemGroupAttributesAppService)
        {
            _itemGroupAttributesAppService = itemGroupAttributesAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemGroupAttributeDto> GetAsync(Guid id)
        {
            return _itemGroupAttributesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ItemGroupAttributeDto> CreateAsync(ItemGroupAttributeCreateDto input)
        {
            return _itemGroupAttributesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemGroupAttributeDto> UpdateAsync(Guid id, ItemGroupAttributeUpdateDto input)
        {
            return _itemGroupAttributesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemGroupAttributesAppService.DeleteAsync(id);
        }
    }
}
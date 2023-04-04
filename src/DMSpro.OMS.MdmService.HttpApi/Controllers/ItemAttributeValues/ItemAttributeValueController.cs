using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.ItemAttributeValues;

namespace DMSpro.OMS.MdmService.Controllers.ItemAttributeValues
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemAttributeValue")]
    [Route("api/mdm-service/item-attribute-values")]
    public partial class ItemAttributeValueController : AbpController, IItemAttributeValuesAppService
    {
        private readonly IItemAttributeValuesAppService _itemAttributeValuesAppService;

        public ItemAttributeValueController(IItemAttributeValuesAppService itemAttributeValuesAppService)
        {
            _itemAttributeValuesAppService = itemAttributeValuesAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemAttributeValueDto> GetAsync(Guid id)
        {
            return _itemAttributeValuesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ItemAttributeValueDto> CreateAsync(ItemAttributeValueCreateDto input)
        {
            return _itemAttributeValuesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemAttributeValueDto> UpdateAsync(Guid id, ItemAttributeValueUpdateDto input)
        {
            return _itemAttributeValuesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemAttributeValuesAppService.DeleteAsync(id);
        }
    }
}
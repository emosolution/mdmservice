using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.ItemAttributes;
using DevExtreme.AspNet.Data.ResponseModel;

namespace DMSpro.OMS.MdmService.Controllers.ItemAttributes
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemAttribute")]
    [Route("api/mdm-service/item-attributes")]
    public partial class ItemAttributeController : AbpController, IItemAttributesAppService
    {
        private readonly IItemAttributesAppService _itemAttributesAppService;

        public ItemAttributeController(IItemAttributesAppService itemAttributesAppService)
        {
            _itemAttributesAppService = itemAttributesAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemAttributeDto> GetAsync(Guid id)
        {
            return _itemAttributesAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<LoadResult> UpdateAsync(Guid id, ItemAttributeUpdateDto input)
        {
            return _itemAttributesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        public virtual Task<LoadResult> DeleteAsync()
        {
            return _itemAttributesAppService.DeleteAsync();
        }

        [HttpPost]
        [Route("hierarchy")]
        public Task<LoadResult> CreateHierarchyAsync(ItemAttributeCreateDto input)
        {
            return _itemAttributesAppService.CreateHierarchyAsync(input);
        }

        [HttpPost]
        [Route("flat")]
        public Task<LoadResult> CreateFlatAsync(ItemAttributeCreateDto input)
        {
            return _itemAttributesAppService.CreateFlatAsync(input);
        }

        [HttpDelete]
        [Route("reset")]
        public Task<LoadResult> ResetAsync()
        {
            return _itemAttributesAppService.ResetAsync();
        }
    }
}
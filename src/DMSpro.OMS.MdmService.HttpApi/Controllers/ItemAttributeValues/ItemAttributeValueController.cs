using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemAttributeValues;
using Volo.Abp.Content;

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
        public Task<PagedResultDto<ItemAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetItemAttributeValuesInput input)
        {
            return _itemAttributeValuesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ItemAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _itemAttributeValuesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemAttributeValueDto> GetAsync(Guid id)
        {
            return _itemAttributeValuesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("item-attribute-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemAttributeLookupAsync(LookupRequestDto input)
        {
            return _itemAttributeValuesAppService.GetItemAttributeLookupAsync(input);
        }

        [HttpGet]
        [Route("item-attribute-value-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetItemAttributeValueLookupAsync(LookupRequestDto input)
        {
            return _itemAttributeValuesAppService.GetItemAttributeValueLookupAsync(input);
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

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttributeValueExcelDownloadDto input)
        {
            return _itemAttributeValuesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemAttributeValuesAppService.GetDownloadTokenAsync();
        }
    }
}
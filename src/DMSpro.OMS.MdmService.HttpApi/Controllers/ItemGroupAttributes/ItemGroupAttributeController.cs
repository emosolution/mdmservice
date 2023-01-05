using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using Volo.Abp.Content;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;

namespace DMSpro.OMS.MdmService.Controllers.ItemGroupAttributes
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemGroupAttribute")]
    [Route("api/mdm-service/item-group-attributes")]
    public class ItemGroupAttributeController : AbpController, IItemGroupAttributesAppService
    {
        private readonly IItemGroupAttributesAppService _itemGroupAttributesAppService;

        public ItemGroupAttributeController(IItemGroupAttributesAppService itemGroupAttributesAppService)
        {
            _itemGroupAttributesAppService = itemGroupAttributesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<ItemGroupAttributeWithNavigationPropertiesDto>> GetListAsync(GetItemGroupAttributesInput input)
        {
            return _itemGroupAttributesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ItemGroupAttributeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _itemGroupAttributesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _itemGroupAttributesAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemGroupAttributeDto> GetAsync(Guid id)
        {
            return _itemGroupAttributesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("item-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
        {
            return _itemGroupAttributesAppService.GetItemGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("item-attribute-value-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetItemAttributeValueLookupAsync(LookupRequestDto input)
        {
            return _itemGroupAttributesAppService.GetItemAttributeValueLookupAsync(input);
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

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupAttributeExcelDownloadDto input)
        {
            return _itemGroupAttributesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemGroupAttributesAppService.GetDownloadTokenAsync();
        }
    }
}
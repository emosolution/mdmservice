using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemGroupAttrs;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.ItemGroupAttrs
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemGroupAttr")]
    [Route("api/mdm-service/item-group-attrs")]
    public class ItemGroupAttrController : AbpController, IItemGroupAttrsAppService
    {
        private readonly IItemGroupAttrsAppService _itemGroupAttrsAppService;

        public ItemGroupAttrController(IItemGroupAttrsAppService itemGroupAttrsAppService)
        {
            _itemGroupAttrsAppService = itemGroupAttrsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<ItemGroupAttrWithNavigationPropertiesDto>> GetListAsync(GetItemGroupAttrsInput input)
        {
            return _itemGroupAttrsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ItemGroupAttrWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _itemGroupAttrsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _itemGroupAttrsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemGroupAttrDto> GetAsync(Guid id)
        {
            return _itemGroupAttrsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("item-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
        {
            return _itemGroupAttrsAppService.GetItemGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("prod-attribute-value-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetProdAttributeValueLookupAsync(LookupRequestDto input)
        {
            return _itemGroupAttrsAppService.GetProdAttributeValueLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<ItemGroupAttrDto> CreateAsync(ItemGroupAttrCreateDto input)
        {
            return _itemGroupAttrsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemGroupAttrDto> UpdateAsync(Guid id, ItemGroupAttrUpdateDto input)
        {
            return _itemGroupAttrsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemGroupAttrsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupAttrExcelDownloadDto input)
        {
            return _itemGroupAttrsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemGroupAttrsAppService.GetDownloadTokenAsync();
        }
    }
}
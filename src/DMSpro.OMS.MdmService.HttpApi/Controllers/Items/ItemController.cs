using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Items;
using Volo.Abp.Content;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;

namespace DMSpro.OMS.MdmService.Controllers.Items
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("Item")]
    [Route("api/mdm-service/items")]
    public class ItemController : AbpController, IItemsAppService
    {
        private readonly IItemsAppService _itemsAppService;

        public ItemController(IItemsAppService itemsAppService)
        {
            _itemsAppService = itemsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<ItemWithNavigationPropertiesDto>> GetListAsync(GetItemsInput input)
        {
            return _itemsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ItemWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _itemsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _itemsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemDto> GetAsync(Guid id)
        {
            return _itemsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("system-data-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            return _itemsAppService.GetSystemDataLookupAsync(input);
        }

        [HttpGet]
        [Route("v-aT-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetVATLookupAsync(LookupRequestDto input)
        {
            return _itemsAppService.GetVATLookupAsync(input);
        }

        [HttpGet]
        [Route("u-oMGroup-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupLookupAsync(LookupRequestDto input)
        {
            return _itemsAppService.GetUOMGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("u-oMGroup-detail-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupDetailLookupAsync(LookupRequestDto input)
        {
            return _itemsAppService.GetUOMGroupDetailLookupAsync(input);
        }

        [HttpGet]
        [Route("item-attribute-value-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetItemAttributeValueLookupAsync(LookupRequestDto input)
        {
            return _itemsAppService.GetItemAttributeValueLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<ItemDto> CreateAsync(ItemCreateDto input)
        {
            return _itemsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemDto> UpdateAsync(Guid id, ItemUpdateDto input)
        {
            return _itemsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemExcelDownloadDto input)
        {
            return _itemsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemsAppService.GetDownloadTokenAsync();
        }
    }
}
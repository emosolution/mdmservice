using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemMasters;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.ItemMasters
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemMaster")]
    [Route("api/mdm-service/item-masters")]
    public class ItemMasterController : AbpController, IItemMastersAppService
    {
        private readonly IItemMastersAppService _itemMastersAppService;

        public ItemMasterController(IItemMastersAppService itemMastersAppService)
        {
            _itemMastersAppService = itemMastersAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<ItemMasterWithNavigationPropertiesDto>> GetListAsync(GetItemMastersInput input)
        {
            return _itemMastersAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ItemMasterWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _itemMastersAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemMasterDto> GetAsync(Guid id)
        {
            return _itemMastersAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _itemMastersAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("system-data-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            return _itemMastersAppService.GetSystemDataLookupAsync(input);
        }

        [HttpGet]
        [Route("v-aT-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetVATLookupAsync(LookupRequestDto input)
        {
            return _itemMastersAppService.GetVATLookupAsync(input);
        }

        [HttpGet]
        [Route("u-oMGroup-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupLookupAsync(LookupRequestDto input)
        {
            return _itemMastersAppService.GetUOMGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("u-oM-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input)
        {
            return _itemMastersAppService.GetUOMLookupAsync(input);
        }

        [HttpGet]
        [Route("prod-attribute-value-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetProdAttributeValueLookupAsync(LookupRequestDto input)
        {
            return _itemMastersAppService.GetProdAttributeValueLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<ItemMasterDto> CreateAsync(ItemMasterCreateDto input)
        {
            return _itemMastersAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemMasterDto> UpdateAsync(Guid id, ItemMasterUpdateDto input)
        {
            return _itemMastersAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemMastersAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemMasterExcelDownloadDto input)
        {
            return _itemMastersAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemMastersAppService.GetDownloadTokenAsync();
        }
    }
}
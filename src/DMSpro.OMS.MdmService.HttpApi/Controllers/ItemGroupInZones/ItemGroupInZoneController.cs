using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemGroupInZones;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.ItemGroupInZones
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemGroupInZone")]
    [Route("api/mdm-service/item-group-in-zones")]
    public class ItemGroupInZoneController : AbpController, IItemGroupInZonesAppService
    {
        private readonly IItemGroupInZonesAppService _itemGroupInZonesAppService;

        public ItemGroupInZoneController(IItemGroupInZonesAppService itemGroupInZonesAppService)
        {
            _itemGroupInZonesAppService = itemGroupInZonesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<ItemGroupInZoneWithNavigationPropertiesDto>> GetListAsync(GetItemGroupInZonesInput input)
        {
            return _itemGroupInZonesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ItemGroupInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _itemGroupInZonesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemGroupInZoneDto> GetAsync(Guid id)
        {
            return _itemGroupInZonesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _itemGroupInZonesAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpGet]
        [Route("item-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
        {
            return _itemGroupInZonesAppService.GetItemGroupLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<ItemGroupInZoneDto> CreateAsync(ItemGroupInZoneCreateDto input)
        {
            return _itemGroupInZonesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemGroupInZoneDto> UpdateAsync(Guid id, ItemGroupInZoneUpdateDto input)
        {
            return _itemGroupInZonesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemGroupInZonesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupInZoneExcelDownloadDto input)
        {
            return _itemGroupInZonesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemGroupInZonesAppService.GetDownloadTokenAsync();
        }
    }
}
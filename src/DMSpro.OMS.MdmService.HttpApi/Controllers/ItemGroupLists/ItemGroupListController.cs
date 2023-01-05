using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemGroupLists;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.ItemGroupLists
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemGroupList")]
    [Route("api/mdm-service/item-group-lists")]
    public class ItemGroupListController : AbpController, IItemGroupListsAppService
    {
        private readonly IItemGroupListsAppService _itemGroupListsAppService;

        public ItemGroupListController(IItemGroupListsAppService itemGroupListsAppService)
        {
            _itemGroupListsAppService = itemGroupListsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<ItemGroupListWithNavigationPropertiesDto>> GetListAsync(GetItemGroupListsInput input)
        {
            return _itemGroupListsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ItemGroupListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _itemGroupListsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemGroupListDto> GetAsync(Guid id)
        {
            return _itemGroupListsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("item-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
        {
            return _itemGroupListsAppService.GetItemGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("item-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input)
        {
            return _itemGroupListsAppService.GetItemLookupAsync(input);
        }

        [HttpGet]
        [Route("u-oM-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input)
        {
            return _itemGroupListsAppService.GetUOMLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<ItemGroupListDto> CreateAsync(ItemGroupListCreateDto input)
        {
            return _itemGroupListsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemGroupListDto> UpdateAsync(Guid id, ItemGroupListUpdateDto input)
        {
            return _itemGroupListsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemGroupListsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupListExcelDownloadDto input)
        {
            return _itemGroupListsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemGroupListsAppService.GetDownloadTokenAsync();
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemGroups;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.ItemGroups
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemGroup")]
    [Route("api/mdm-service/item-groups")]
    public class ItemGroupController : AbpController, IItemGroupsAppService
    {
        private readonly IItemGroupsAppService _itemGroupsAppService;

        public ItemGroupController(IItemGroupsAppService itemGroupsAppService)
        {
            _itemGroupsAppService = itemGroupsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ItemGroupDto>> GetListAsync(GetItemGroupsInput input)
        {
            return _itemGroupsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _itemGroupsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemGroupDto> GetAsync(Guid id)
        {
            return _itemGroupsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ItemGroupDto> CreateAsync(ItemGroupCreateDto input)
        {
            return _itemGroupsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemGroupDto> UpdateAsync(Guid id, ItemGroupUpdateDto input)
        {
            return _itemGroupsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemGroupsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupExcelDownloadDto input)
        {
            return _itemGroupsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemGroupsAppService.GetDownloadTokenAsync();
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.ItemGroups;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.ItemGroups
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.ItemGroups.Default)]
    public class ItemGroupsAppService : ApplicationService, IItemGroupsAppService
    {
        private readonly IDistributedCache<ItemGroupExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly ItemGroupManager _itemGroupManager;

        public ItemGroupsAppService(IItemGroupRepository itemGroupRepository, ItemGroupManager itemGroupManager, IDistributedCache<ItemGroupExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemGroupRepository = itemGroupRepository;
            _itemGroupManager = itemGroupManager;
        }

        public virtual async Task<PagedResultDto<ItemGroupDto>> GetListAsync(GetItemGroupsInput input)
        {
            var totalCount = await _itemGroupRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Description, input.Type, input.Status);
            var items = await _itemGroupRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Description, input.Type, input.Status, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemGroupDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemGroup>, List<ItemGroupDto>>(items)
            };
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _itemGroupRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<ItemGroup>, IEnumerable<ItemGroupDto>>(results.data.Cast<ItemGroup>());
            
            return results;
                
        }
        public virtual async Task<ItemGroupDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroup, ItemGroupDto>(await _itemGroupRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemGroupRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Create)]
        public virtual async Task<ItemGroupDto> CreateAsync(ItemGroupCreateDto input)
        {

            var itemGroup = await _itemGroupManager.CreateAsync(
            input.Code, input.Name, input.Description, input.Type, input.Status
            );

            return ObjectMapper.Map<ItemGroup, ItemGroupDto>(itemGroup);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Edit)]
        public virtual async Task<ItemGroupDto> UpdateAsync(Guid id, ItemGroupUpdateDto input)
        {

            var itemGroup = await _itemGroupManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Description, input.Type, input.Status, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemGroup, ItemGroupDto>(itemGroup);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _itemGroupRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Description, input.Type, input.Status);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ItemGroup>, List<ItemGroupExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ItemGroups.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemGroupExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}
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
namespace DMSpro.OMS.MdmService.ItemAttributes
{

    [Authorize(MdmServicePermissions.ItemAttributes.Default)]
    public class ItemAttributesAppService : ApplicationService, IItemAttributesAppService
    {
        private readonly IDistributedCache<ItemAttributeExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemAttributeRepository _itemAttributeRepository;
        private readonly ItemAttributeManager _itemAttributeManager;

        public ItemAttributesAppService(IItemAttributeRepository itemAttributeRepository, ItemAttributeManager itemAttributeManager, IDistributedCache<ItemAttributeExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemAttributeRepository = itemAttributeRepository;
            _itemAttributeManager = itemAttributeManager;
        }

        public virtual async Task<PagedResultDto<ItemAttributeDto>> GetListAsync(GetItemAttributesInput input)
        {
            var totalCount = await _itemAttributeRepository.GetCountAsync(input.FilterText, input.AttrNoMin, input.AttrNoMax, input.AttrName, input.HierarchyLevelMin, input.HierarchyLevelMax, input.Active, input.IsSellingCategory);
            var items = await _itemAttributeRepository.GetListAsync(input.FilterText, input.AttrNoMin, input.AttrNoMax, input.AttrName, input.HierarchyLevelMin, input.HierarchyLevelMax, input.Active, input.IsSellingCategory, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemAttributeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemAttribute>, List<ItemAttributeDto>>(items)
            };
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _itemAttributeRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<ItemAttribute>, IEnumerable<ItemAttributeDto>>(results.data.Cast<ItemAttribute>());
            
            return results;
                
        }

        public virtual async Task<ItemAttributeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(await _itemAttributeRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemAttributeRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Create)]
        public virtual async Task<ItemAttributeDto> CreateAsync(ItemAttributeCreateDto input)
        {

            var itemAttribute = await _itemAttributeManager.CreateAsync(
            input.AttrNo, input.AttrName, input.Active, input.IsSellingCategory, input.HierarchyLevel
            );

            return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(itemAttribute);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Edit)]
        public virtual async Task<ItemAttributeDto> UpdateAsync(Guid id, ItemAttributeUpdateDto input)
        {

            var itemAttribute = await _itemAttributeManager.UpdateAsync(
            id,
            input.AttrNo, input.AttrName, input.Active, input.IsSellingCategory, input.HierarchyLevel, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(itemAttribute);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttributeExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _itemAttributeRepository.GetListAsync(input.FilterText, input.AttrNoMin, input.AttrNoMax, input.AttrName, input.HierarchyLevelMin, input.HierarchyLevelMax, input.Active, input.IsSellingCategory);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ItemAttribute>, List<ItemAttributeExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ItemAttributes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemAttributeExcelDownloadTokenCacheItem { Token = token },
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
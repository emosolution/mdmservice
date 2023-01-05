using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.ItemGroups;
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

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.MdmService.ItemAttributes;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{

    [Authorize(MdmServicePermissions.ItemGroups.Default)]
    public class ItemGroupAttributesAppService : ApplicationService, IItemGroupAttributesAppService
    {
        private readonly IDistributedCache<ItemGroupAttributeExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;
        private readonly ItemGroupAttributeManager _itemGroupAttributeManager;
        private readonly IRepository<ItemGroup, Guid> _itemGroupRepository;
        private readonly IRepository<ItemAttributeValue, Guid> _itemAttributeValueRepository;

        public ItemGroupAttributesAppService(IItemGroupAttributeRepository itemGroupAttributeRepository, ItemGroupAttributeManager itemGroupAttributeManager, IDistributedCache<ItemGroupAttributeExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<ItemGroup, Guid> itemGroupRepository, IRepository<ItemAttributeValue, Guid> itemAttributeValueRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemGroupAttributeRepository = itemGroupAttributeRepository;
            _itemGroupAttributeManager = itemGroupAttributeManager; _itemGroupRepository = itemGroupRepository;
            _itemAttributeValueRepository = itemAttributeValueRepository;
        }

        public virtual async Task<PagedResultDto<ItemGroupAttributeWithNavigationPropertiesDto>> GetListAsync(GetItemGroupAttributesInput input)
        {
            var totalCount = await _itemGroupAttributeRepository.GetCountAsync(input.FilterText, input.dummy, input.ItemGroupId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Attr5Id);
            var items = await _itemGroupAttributeRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.dummy, input.ItemGroupId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Attr5Id, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemGroupAttributeWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemGroupAttributeWithNavigationProperties>, List<ItemGroupAttributeWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ItemGroupAttributeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroupAttributeWithNavigationProperties, ItemGroupAttributeWithNavigationPropertiesDto>
                (await _itemGroupAttributeRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _itemGroupAttributeRepository.GetQueryableAsync();
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            results.data = ObjectMapper.Map<IEnumerable<ItemAttribute>, IEnumerable<ItemAttributeDto>>(results.data.Cast<ItemAttribute>());

            return results;

        }

        public virtual async Task<ItemGroupAttributeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroupAttribute, ItemGroupAttributeDto>(await _itemGroupAttributeRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ItemGroup>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemGroup>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetItemAttributeValueLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemAttributeValueRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrValName != null &&
                         x.AttrValName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ItemAttributeValue>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemAttributeValue>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.ItemGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemGroupAttributeRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Create)]
        public virtual async Task<ItemGroupAttributeDto> CreateAsync(ItemGroupAttributeCreateDto input)
        {
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }

            var itemGroupAttribute = await _itemGroupAttributeManager.CreateAsync(
            input.ItemGroupId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Attr5Id, input.dummy
            );

            return ObjectMapper.Map<ItemGroupAttribute, ItemGroupAttributeDto>(itemGroupAttribute);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Edit)]
        public virtual async Task<ItemGroupAttributeDto> UpdateAsync(Guid id, ItemGroupAttributeUpdateDto input)
        {
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }

            var itemGroupAttribute = await _itemGroupAttributeManager.UpdateAsync(
            id,
            input.ItemGroupId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Attr5Id, input.dummy, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemGroupAttribute, ItemGroupAttributeDto>(itemGroupAttribute);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupAttributeExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _itemGroupAttributeRepository.GetListAsync(input.FilterText, input.dummy);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ItemGroupAttribute>, List<ItemGroupAttributeExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ItemGroupAttributes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemGroupAttributeExcelDownloadTokenCacheItem { Token = token },
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
using DMSpro.OMS.MdmService.ProdAttributeValues;
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
using DMSpro.OMS.MdmService.ItemGroupAttrs;
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
namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.ItemGroupAttrs.Default)]
    public class ItemGroupAttrsAppService : ApplicationService, IItemGroupAttrsAppService
    {
        private readonly IDistributedCache<ItemGroupAttrExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemGroupAttrRepository _itemGroupAttrRepository;
        private readonly ItemGroupAttrManager _itemGroupAttrManager;
        private readonly IRepository<ItemGroup, Guid> _itemGroupRepository;
        private readonly IRepository<ProdAttributeValue, Guid> _prodAttributeValueRepository;

        public ItemGroupAttrsAppService(IItemGroupAttrRepository itemGroupAttrRepository, ItemGroupAttrManager itemGroupAttrManager, IDistributedCache<ItemGroupAttrExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<ItemGroup, Guid> itemGroupRepository, IRepository<ProdAttributeValue, Guid> prodAttributeValueRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemGroupAttrRepository = itemGroupAttrRepository;
            _itemGroupAttrManager = itemGroupAttrManager; _itemGroupRepository = itemGroupRepository;
            _prodAttributeValueRepository = prodAttributeValueRepository;
        }

        public virtual async Task<PagedResultDto<ItemGroupAttrWithNavigationPropertiesDto>> GetListAsync(GetItemGroupAttrsInput input)
        {
            var totalCount = await _itemGroupAttrRepository.GetCountAsync(input.FilterText, input.Dummy, input.ItemGroupId, input.Attr0, input.Attr1, input.Attr2, input.Attr3, input.Attr4, input.Attr5, input.Attr6, input.Attr7, input.Attr8, input.Attr9, input.Attr10, input.Attr11, input.Attr12, input.Attr13, input.Attr14, input.Attr15, input.Attr16, input.Attr17, input.Attr18, input.Attr19);
            var items = await _itemGroupAttrRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Dummy, input.ItemGroupId, input.Attr0, input.Attr1, input.Attr2, input.Attr3, input.Attr4, input.Attr5, input.Attr6, input.Attr7, input.Attr8, input.Attr9, input.Attr10, input.Attr11, input.Attr12, input.Attr13, input.Attr14, input.Attr15, input.Attr16, input.Attr17, input.Attr18, input.Attr19, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemGroupAttrWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemGroupAttrWithNavigationProperties>, List<ItemGroupAttrWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ItemGroupAttrWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroupAttrWithNavigationProperties, ItemGroupAttrWithNavigationPropertiesDto>
                (await _itemGroupAttrRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _itemGroupAttrRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<ItemGroupAttr>, IEnumerable<ItemGroupAttrDto>>(results.data.Cast<ItemGroupAttr>());
            
            return results;
                
        }
        public virtual async Task<ItemGroupAttrDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroupAttr, ItemGroupAttrDto>(await _itemGroupAttrRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetProdAttributeValueLookupAsync(LookupRequestDto input)
        {
            var query = (await _prodAttributeValueRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrValName != null &&
                         x.AttrValName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ProdAttributeValue>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ProdAttributeValue>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.ItemGroupAttrs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemGroupAttrRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemGroupAttrs.Create)]
        public virtual async Task<ItemGroupAttrDto> CreateAsync(ItemGroupAttrCreateDto input)
        {
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }

            var itemGroupAttr = await _itemGroupAttrManager.CreateAsync(
            input.ItemGroupId, input.Attr0, input.Attr1, input.Attr2, input.Attr3, input.Attr4, input.Attr5, input.Attr6, input.Attr7, input.Attr8, input.Attr9, input.Attr10, input.Attr11, input.Attr12, input.Attr13, input.Attr14, input.Attr15, input.Attr16, input.Attr17, input.Attr18, input.Attr19, input.Dummy
            );

            return ObjectMapper.Map<ItemGroupAttr, ItemGroupAttrDto>(itemGroupAttr);
        }

        [Authorize(MdmServicePermissions.ItemGroupAttrs.Edit)]
        public virtual async Task<ItemGroupAttrDto> UpdateAsync(Guid id, ItemGroupAttrUpdateDto input)
        {
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }

            var itemGroupAttr = await _itemGroupAttrManager.UpdateAsync(
            id,
            input.ItemGroupId, input.Attr0, input.Attr1, input.Attr2, input.Attr3, input.Attr4, input.Attr5, input.Attr6, input.Attr7, input.Attr8, input.Attr9, input.Attr10, input.Attr11, input.Attr12, input.Attr13, input.Attr14, input.Attr15, input.Attr16, input.Attr17, input.Attr18, input.Attr19, input.Dummy, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemGroupAttr, ItemGroupAttrDto>(itemGroupAttr);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupAttrExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _itemGroupAttrRepository.GetListAsync(input.FilterText, input.Dummy);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ItemGroupAttr>, List<ItemGroupAttrExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ItemGroupAttrs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemGroupAttrExcelDownloadTokenCacheItem { Token = token },
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

using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.Items;
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

namespace DMSpro.OMS.MdmService.ItemGroupLists
{

    [Authorize(MdmServicePermissions.ItemGroups.Default)]
    public partial class ItemGroupListsAppService : ApplicationService, IItemGroupListsAppService
    {
        private readonly IDistributedCache<ItemGroupListExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemGroupListRepository _itemGroupListRepository;
        private readonly ItemGroupListManager _itemGroupListManager;
        private readonly IRepository<ItemGroup, Guid> _itemGroupRepository;
        private readonly IRepository<Item, Guid> _itemRepository;
        private readonly IRepository<UOM, Guid> _uOMRepository;

        public ItemGroupListsAppService(IItemGroupListRepository itemGroupListRepository, ItemGroupListManager itemGroupListManager, IDistributedCache<ItemGroupListExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<ItemGroup, Guid> itemGroupRepository, IRepository<Item, Guid> itemRepository, IRepository<UOM, Guid> uOMRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemGroupListRepository = itemGroupListRepository;
            _itemGroupListManager = itemGroupListManager; _itemGroupRepository = itemGroupRepository;
            _itemRepository = itemRepository;
            _uOMRepository = uOMRepository;
        }

        public virtual async Task<PagedResultDto<ItemGroupListWithNavigationPropertiesDto>> GetListAsync(GetItemGroupListsInput input)
        {
            var totalCount = await _itemGroupListRepository.GetCountAsync(input.FilterText, input.RateMin, input.RateMax, input.PriceMin, input.PriceMax, input.ItemGroupId, input.ItemId, input.UomId);
            var items = await _itemGroupListRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.RateMin, input.RateMax, input.PriceMin, input.PriceMax, input.ItemGroupId, input.ItemId, input.UomId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemGroupListWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemGroupListWithNavigationProperties>, List<ItemGroupListWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ItemGroupListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroupListWithNavigationProperties, ItemGroupListWithNavigationPropertiesDto>
                (await _itemGroupListRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ItemGroupListDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroupList, ItemGroupListDto>(await _itemGroupListRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Item>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Item>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input)
        {
            var query = (await _uOMRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<UOM>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOM>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.ItemGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemGroupListRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Create)]
        public virtual async Task<ItemGroupListDto> CreateAsync(ItemGroupListCreateDto input)
        {
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }
            if (input.UomId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var itemGroupList = await _itemGroupListManager.CreateAsync(
            input.ItemGroupId, input.ItemId, input.UomId, input.Rate, input.Price
            );

            return ObjectMapper.Map<ItemGroupList, ItemGroupListDto>(itemGroupList);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Edit)]
        public virtual async Task<ItemGroupListDto> UpdateAsync(Guid id, ItemGroupListUpdateDto input)
        {
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }
            if (input.UomId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var itemGroupList = await _itemGroupListManager.UpdateAsync(
            id,
            input.ItemGroupId, input.ItemId, input.UomId, input.Rate, input.Price, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemGroupList, ItemGroupListDto>(itemGroupList);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupListExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _itemGroupListRepository.GetListAsync(input.FilterText, input.RateMin, input.RateMax, input.PriceMin, input.PriceMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ItemGroupList>, List<ItemGroupListExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ItemGroupLists.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemGroupListExcelDownloadTokenCacheItem { Token = token },
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
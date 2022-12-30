using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.ItemMasters;
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
using DMSpro.OMS.MdmService.ItemGroupLists;
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
namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.ItemGroupLists.Default)]
    public class ItemGroupListsAppService : ApplicationService, IItemGroupListsAppService
    {
        private readonly IDistributedCache<ItemGroupListExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemGroupListRepository _itemGroupListRepository;
        private readonly ItemGroupListManager _itemGroupListManager;
        private readonly IRepository<ItemGroup, Guid> _itemGroupRepository;
        private readonly IRepository<ItemMaster, Guid> _itemMasterRepository;
        private readonly IRepository<UOM, Guid> _uOMRepository;

        public ItemGroupListsAppService(IItemGroupListRepository itemGroupListRepository, ItemGroupListManager itemGroupListManager, IDistributedCache<ItemGroupListExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<ItemGroup, Guid> itemGroupRepository, IRepository<ItemMaster, Guid> itemMasterRepository, IRepository<UOM, Guid> uOMRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemGroupListRepository = itemGroupListRepository;
            _itemGroupListManager = itemGroupListManager; _itemGroupRepository = itemGroupRepository;
            _itemMasterRepository = itemMasterRepository;
            _uOMRepository = uOMRepository;
        }

        public virtual async Task<PagedResultDto<ItemGroupListWithNavigationPropertiesDto>> GetListAsync(GetItemGroupListsInput input)
        {
            var totalCount = await _itemGroupListRepository.GetCountAsync(input.FilterText, input.RateMin, input.RateMax, input.ItemGroupId, input.ItemId, input.UOMId);
            var items = await _itemGroupListRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.RateMin, input.RateMax, input.ItemGroupId, input.ItemId, input.UOMId, input.Sorting, input.MaxResultCount, input.SkipCount);

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

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _itemGroupListRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<ItemGroupList>, IEnumerable<ItemGroupListDto>>(results.data.Cast<ItemGroupList>());
            
            return results;
                
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemMasterLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemMasterRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ItemMaster>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemMaster>, List<LookupDto<Guid>>>(lookupData)
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

        [Authorize(MdmServicePermissions.ItemGroupLists.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemGroupListRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemGroupLists.Create)]
        public virtual async Task<ItemGroupListDto> CreateAsync(ItemGroupListCreateDto input)
        {
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemMaster"]]);
            }
            if (input.UOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var itemGroupList = await _itemGroupListManager.CreateAsync(
            input.ItemGroupId, input.ItemId, input.UOMId, input.Rate
            );

            return ObjectMapper.Map<ItemGroupList, ItemGroupListDto>(itemGroupList);
        }

        [Authorize(MdmServicePermissions.ItemGroupLists.Edit)]
        public virtual async Task<ItemGroupListDto> UpdateAsync(Guid id, ItemGroupListUpdateDto input)
        {
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemMaster"]]);
            }
            if (input.UOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var itemGroupList = await _itemGroupListManager.UpdateAsync(
            id,
            input.ItemGroupId, input.ItemId, input.UOMId, input.Rate, input.ConcurrencyStamp
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

            var items = await _itemGroupListRepository.GetListAsync(input.FilterText, input.RateMin, input.RateMax);

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

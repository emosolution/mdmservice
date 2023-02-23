using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.ItemGroups;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    [Authorize(MdmServicePermissions.ItemGroups.Default)]
    public partial class ItemGroupListsAppService : PartialAppService<ItemGroupList, ItemGroupListWithDetailsDto, IItemGroupListRepository>,
        IItemGroupListsAppService
    {
        private readonly IItemGroupListRepository _itemGroupListRepository;
        private readonly IDistributedCache<ItemGroupListExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly ItemGroupListManager _itemGroupListManager;

        private readonly IUOMRepository _uOMRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IItemGroupRepository _itemGroupRepository;

        public ItemGroupListsAppService(ICurrentTenant currentTenant,
            IItemGroupListRepository repository,
            ItemGroupListManager itemGroupListManager,
            IConfiguration settingProvider,
            IUOMRepository uOMRepository,
            IItemRepository itemRepository,
            IItemGroupRepository itemGroupRepository,
            IDistributedCache<ItemGroupListExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _itemGroupListRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemGroupListManager = itemGroupListManager;

            _uOMRepository = uOMRepository;
            _itemRepository = itemRepository;
            _itemGroupRepository = itemGroupRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupListRepository", _itemGroupListRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMRepository", _uOMRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemRepository", _itemRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupRepository", _itemGroupRepository));
        }
    }
}
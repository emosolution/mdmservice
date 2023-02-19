using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.ItemAttributeValues;

namespace DMSpro.OMS.MdmService.Items
{
    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemsAppService : PartialAppService<Item, ItemDto, IItemRepository>,
        IItemsAppService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IDistributedCache<ItemExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly ItemManager _itemManager;

        private readonly IVATRepository _vATRepository;
        private readonly IUOMGroupRepository _uOMGroupRepository;
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;
        private readonly ISystemDataRepository _systemDataRepository;
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;

        public ItemsAppService(ICurrentTenant currentTenant,
            IItemRepository repository,
            ItemManager itemManager,
            IConfiguration settingProvider,
            IVATRepository vATRepository,
            IUOMGroupRepository uOMGroupRepository,
            IItemAttributeValueRepository itemAttributeValueRepository,
            ISystemDataRepository systemDataRepository,
            IUOMGroupDetailRepository uOMGroupDetailRepository,
            IDistributedCache<ItemExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _itemRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemManager = itemManager;

            _vATRepository = vATRepository;
            _systemDataRepository = systemDataRepository;
            _itemAttributeValueRepository = itemAttributeValueRepository;
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
            _uOMGroupRepository = uOMGroupRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemRepository", _itemRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IVATRepository", _vATRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMGroupRepository", _uOMGroupRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMGroupDetailRepository", _uOMGroupDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISystemDataRepository", _systemDataRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemAttributeValueRepository", _itemAttributeValueRepository));
        }
    }
}
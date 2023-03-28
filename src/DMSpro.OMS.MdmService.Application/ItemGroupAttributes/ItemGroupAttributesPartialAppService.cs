using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.ItemAttributeValues;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    [Authorize(MdmServicePermissions.ItemGroups.Default)]
    public partial class ItemGroupAttributesAppService : PartialAppService<ItemGroupAttribute, ItemGroupAttributeDto, IItemGroupAttributeRepository>,
        IItemGroupAttributesAppService
    {
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;
        private readonly IDistributedCache<ItemGroupAttributeExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly ItemGroupAttributeManager _itemGroupAttributeManager;

        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;

        public ItemGroupAttributesAppService(ICurrentTenant currentTenant,
            IItemGroupAttributeRepository repository,
            ItemGroupAttributeManager itemGroupAttributeManager,
            IConfiguration settingProvider,
            IItemGroupRepository itemGroupRepository,
            IItemAttributeValueRepository itemAttributeValueRepository,
            IDistributedCache<ItemGroupAttributeExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.ItemGroups.Default)
        {
            _itemGroupAttributeRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemGroupAttributeManager = itemGroupAttributeManager;

            _itemGroupRepository = itemGroupRepository;
            _itemAttributeValueRepository = itemAttributeValueRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupAttributeRepository", _itemGroupAttributeRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupRepository", _itemGroupRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemAttributeValueRepository", _itemAttributeValueRepository));
        }
    }
}
using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    [Authorize(MdmServicePermissions.ItemGroupInZones.Default)]
    public partial class ItemGroupInZonesAppService : PartialAppService<ItemGroupInZone, ItemGroupInZoneWithDetailsDto, IItemGroupInZoneRepository>,
        IItemGroupInZonesAppService
    {
        private readonly IItemGroupInZoneRepository _itemGroupInZoneRepository;
        private readonly IDistributedCache<ItemGroupInZoneExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly ItemGroupInZoneManager _itemGroupInZoneManager;

        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly IItemGroupRepository _itemGroupRepository;


        public ItemGroupInZonesAppService(ICurrentTenant currentTenant,
            IItemGroupInZoneRepository repository,
            ItemGroupInZoneManager itemGroupInZoneManager,
            IConfiguration settingProvider,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            IItemGroupRepository itemGroupRepository,
            IDistributedCache<ItemGroupInZoneExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _itemGroupInZoneRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemGroupInZoneManager = itemGroupInZoneManager;

            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _itemGroupRepository = itemGroupRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupInZoneRepository", _itemGroupInZoneRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupRepository", _itemGroupRepository));
        }

    }
}
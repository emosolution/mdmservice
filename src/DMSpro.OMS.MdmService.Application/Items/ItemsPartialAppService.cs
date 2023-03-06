using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using Volo.Abp.Json;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.CompanyInZones;
using DMSpro.OMS.MdmService.ItemGroupInZones;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.ItemGroupLists;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using DMSpro.OMS.MdmService.UOMGroupDetails;

namespace DMSpro.OMS.MdmService.Items
{
    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemsAppService : PartialAppService<Item, ItemWithDetailsDto, IItemRepository>,
        IItemsAppService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IDistributedCache<ItemExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly ItemManager _itemManager;
        private readonly IJsonSerializer _jsonSerializer;

        private readonly IItemAttachmentRepository _itemAttachmentRepository;
        private readonly IItemImageRepository _itemImageRepository;
        //private readonly IItemAttachmentsAppService _itemAttachmentsAppService;
        //private readonly IItemImagesAppService _itemImagesAppService;

        private readonly IVATRepository _vATRepository;
        private readonly IUOMGroupRepository _uOMGroupRepository;
        private readonly ISystemDataRepository _systemDataRepository;
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;
        private readonly IUOMRepository _uOMRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyInZoneRepository _companyInZoneRepository;
        private readonly IItemGroupInZoneRepository _itemGroupInZoneRepository;
        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly IItemGroupListRepository _itemGroupListRepository;
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;

        public ItemsAppService(ICurrentTenant currentTenant,
            IItemRepository repository,
            ItemManager itemManager,
            IJsonSerializer jsonSerializer,
            IItemAttachmentRepository itemAttachmentRepository,
            IItemImageRepository itemImageRepository,
            //IItemAttachmentsAppService itemAttachmentsAppService,
            //IItemImagesAppService itemImagesAppService,
            IConfiguration settingProvider,
            IVATRepository vATRepository,
            IUOMGroupRepository uOMGroupRepository,
            IItemAttributeValueRepository itemAttributeValueRepository,
            IUOMRepository uOMRepository,
            ISystemDataRepository systemDataRepository,
            ICompanyRepository companyRepository,
            ICompanyInZoneRepository companyInZoneRepository,
            IItemGroupInZoneRepository itemGroupInZoneRepository,
            IItemGroupRepository itemGroupRepository,
            IItemGroupListRepository itemGroupListRepository,
            IItemGroupAttributeRepository itemGroupAttributeRepository,
            IUOMGroupDetailRepository uOMGroupDetailRepository,
            IDistributedCache<ItemExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _itemRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemManager = itemManager;
            _jsonSerializer = jsonSerializer;

            _itemAttachmentRepository = itemAttachmentRepository;
            _itemImageRepository = itemImageRepository;
            //_itemAttachmentsAppService = itemAttachmentsAppService;
            //_itemImagesAppService = itemImagesAppService;

            _vATRepository = vATRepository;
            _systemDataRepository = systemDataRepository;
            _itemAttributeValueRepository = itemAttributeValueRepository;
            _uOMGroupRepository = uOMGroupRepository;
            _uOMRepository = uOMRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemRepository", _itemRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IVATRepository", _vATRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMGroupRepository", _uOMGroupRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMRepository", _uOMRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISystemDataRepository", _systemDataRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemAttributeValueRepository", _itemAttributeValueRepository));

            _companyRepository = companyRepository;
            _companyInZoneRepository = companyInZoneRepository;
            _itemGroupInZoneRepository = itemGroupInZoneRepository;
            _itemGroupRepository = itemGroupRepository;
            _itemGroupListRepository = itemGroupListRepository;
            _itemGroupAttributeRepository = itemGroupAttributeRepository;
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
        }
    }
}
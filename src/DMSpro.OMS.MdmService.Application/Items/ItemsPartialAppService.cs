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
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.PriceListDetails;

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

        private readonly IItemAttachmentRepository _itemAttachmentRepository;
        private readonly IItemImageRepository _itemImageRepository;
        //private readonly IItemAttachmentsAppService _itemAttachmentsAppService;
        //private readonly IItemImagesAppService _itemImagesAppService;

        private readonly IVATRepository _vATRepository;
        private readonly IUOMGroupRepository _uOMGroupRepository;
        private readonly ISystemDataRepository _systemDataRepository;
        private readonly IPriceListRepository _priceListRepository;
        private readonly IPriceListDetailRepository _priceListDetailRepository;
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;
        private readonly IUOMRepository _uOMRepository;

        public ItemsAppService(ICurrentTenant currentTenant,
            IItemRepository repository,
            ItemManager itemManager,
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
            IPriceListRepository priceListRepository,
            IPriceListDetailRepository priceListDetailRepository,
            IDistributedCache<ItemExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.Items.Default)
        {
            _itemRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemManager = itemManager;

            _itemAttachmentRepository = itemAttachmentRepository;
            _itemImageRepository = itemImageRepository;
            //_itemAttachmentsAppService = itemAttachmentsAppService;
            //_itemImagesAppService = itemImagesAppService;

            _vATRepository = vATRepository;
            _systemDataRepository = systemDataRepository;
            _priceListRepository = priceListRepository;
            _priceListDetailRepository = priceListDetailRepository;
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
        }
    }
}
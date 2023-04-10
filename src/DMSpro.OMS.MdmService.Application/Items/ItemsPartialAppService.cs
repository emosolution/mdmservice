using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService.Items
{
    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemsAppService : PartialAppService<Item, ItemWithDetailsDto, IItemRepository>,
        IItemsAppService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ItemManager _itemManager;
        private readonly INumberingConfigDetailsInternalAppService _numberingConfigDetailsInternalAppService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IItemAttachmentRepository _itemAttachmentRepository;
        private readonly IItemImageRepository _itemImageRepository;

        private readonly IVATRepository _vATRepository;
        private readonly IUOMGroupRepository _uOMGroupRepository;
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;
        private readonly IPriceListRepository _priceListRepository;
        private readonly IPriceListDetailRepository _priceListDetailRepository;
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;
        private readonly IUOMRepository _uOMRepository;

        private readonly IItemsInternalAppService _itemsInternalAppService;

        public ItemsAppService(ICurrentTenant currentTenant,
            IItemRepository repository,
            ItemManager itemManager,
            INumberingConfigDetailsInternalAppService numberingConfigDetailsInternalAppService,
            ICompanyRepository companyRepository,
            IItemAttachmentRepository itemAttachmentRepository,
            IItemImageRepository itemImageRepository,
            IConfiguration settingProvider,
            IVATRepository vATRepository,
            IUOMGroupRepository uOMGroupRepository,
            IUOMGroupDetailRepository uOMGroupDetailRepository,
            IItemAttributeValueRepository itemAttributeValueRepository,
            IUOMRepository uOMRepository,
            IPriceListRepository priceListRepository,
            IPriceListDetailRepository priceListDetailRepository,
            IItemsInternalAppService itemsInternalAppService)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.Items.Default)
        {
            _itemRepository = repository;
            _itemManager = itemManager;
            _numberingConfigDetailsInternalAppService = numberingConfigDetailsInternalAppService;
            _companyRepository = companyRepository;
            _itemAttachmentRepository = itemAttachmentRepository;
            _itemImageRepository = itemImageRepository;

            _vATRepository = vATRepository;
            _priceListRepository = priceListRepository;
            _priceListDetailRepository = priceListDetailRepository;
            _itemAttributeValueRepository = itemAttributeValueRepository;
            _uOMGroupRepository = uOMGroupRepository;
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
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
                new KeyValuePair<string, object>("IItemAttributeValueRepository", _itemAttributeValueRepository));
            
            _itemsInternalAppService = itemsInternalAppService;
        }
    }
}
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
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.ItemGroupLists;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.CustomerInZones;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.Vendors;
using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;

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
        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly IItemGroupListRepository _itemGroupListRepository;
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly ICustomerInZoneRepository _customerInZoneRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPriceListRepository _priceListRepository;
        private readonly IPriceListDetailRepository _priceListDetailRepository;
        private readonly ISalesOrgEmpAssignmentRepository _salesOrgEmpAssignmentRepository;
        private readonly IEmployeeProfileRepository _employeeProfileRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IMCPHeaderRepository _mcpHeaderRepository;
        private readonly IMCPDetailRepository _mcpDetailRepository;
        private readonly ICompanyIdentityUserAssignmentsAppService _companyIdentityUserAssignmentsAppService;

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
            IItemGroupRepository itemGroupRepository,
            IItemGroupListRepository itemGroupListRepository,
            IItemGroupAttributeRepository itemGroupAttributeRepository,
            IUOMGroupDetailRepository uOMGroupDetailRepository,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            ICustomerInZoneRepository customerInZoneRepository,
            ICustomerRepository customerRepository,
            IPriceListRepository priceListRepository,
            IPriceListDetailRepository priceListDetailRepository,
            ISalesOrgEmpAssignmentRepository salesOrgEmpAssignmentRepository,
            IEmployeeProfileRepository employeeProfileRepository,
            IVendorRepository vendorRepository,
            IMCPHeaderRepository mcpHeaderRepository,
            IMCPDetailRepository mcpDetailRepository,
            ICompanyIdentityUserAssignmentsAppService companyIdentityUserAssignmentsAppService,
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

            _mcpHeaderRepository = mcpHeaderRepository;
            _mcpDetailRepository = mcpDetailRepository;
            _companyIdentityUserAssignmentsAppService = companyIdentityUserAssignmentsAppService;

            _itemGroupRepository = itemGroupRepository;
            _itemGroupListRepository = itemGroupListRepository;
            _itemGroupAttributeRepository = itemGroupAttributeRepository;
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;

            _customerInZoneRepository = customerInZoneRepository;
            _customerRepository = customerRepository;
            _priceListDetailRepository = priceListDetailRepository;
            _priceListRepository = priceListRepository;

            _salesOrgEmpAssignmentRepository = salesOrgEmpAssignmentRepository;
            _employeeProfileRepository = employeeProfileRepository;

            _vendorRepository = vendorRepository;
        }
    }
}
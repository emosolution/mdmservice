using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.CustomerAttachments;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.CustomerAttributeValues;

namespace DMSpro.OMS.MdmService.Customers
{
    [Authorize(MdmServicePermissions.Customers.Default)]
    public partial class CustomersAppService : PartialAppService<Customer, CustomerWithDetailsDto, ICustomerRepository>,
        ICustomersAppService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerManager _customerManager;
        private readonly INumberingConfigDetailsInternalAppService _numberingConfigDetailsInternalAppService;
        private readonly ICustomerAttachmentRepository _customerAttachmentRepository;

        private readonly ISystemDataRepository _systemDataRepository;
        private readonly IPriceListRepository _priceListRepository;
        private readonly IGeoMasterRepository _geoMasterRepository;
        private readonly ICustomerAttributeValueRepository _customerAttributeValueRepository;
        private readonly ICompanyRepository _companyRepository;

        public CustomersAppService(ICurrentTenant currentTenant,
            ICustomerRepository repository,
            CustomerManager customerManager,
            INumberingConfigDetailsInternalAppService numberingConfigDetailsInternalAppService,
            ICustomerAttachmentRepository customerAttachmentRepository,
            IConfiguration settingProvider,
            ISystemDataRepository systemDataRepository,
            IPriceListRepository priceListRepository,
            IGeoMasterRepository geoMasterRepository,
            ICustomerAttributeValueRepository customerAttributeValueRepository,
            ICompanyRepository companyRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.Customers.Default)
        {
            _customerRepository = repository;
            _customerManager = customerManager;
            _numberingConfigDetailsInternalAppService = numberingConfigDetailsInternalAppService;
            _customerAttachmentRepository = customerAttachmentRepository;

            _systemDataRepository = systemDataRepository;
            _priceListRepository = priceListRepository;
            _geoMasterRepository = geoMasterRepository;
            _customerAttributeValueRepository = customerAttributeValueRepository;
            _companyRepository = companyRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISystemDataRepository", _systemDataRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IGeoMasterRepository", _geoMasterRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerAttributeValueRepository", _customerAttributeValueRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
        }
    }
}
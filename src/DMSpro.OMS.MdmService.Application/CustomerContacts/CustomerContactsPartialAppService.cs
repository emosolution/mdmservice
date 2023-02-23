using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    [Authorize(MdmServicePermissions.Customers.Default)]
    public partial class CustomerContactsAppService : PartialAppService<CustomerContact, CustomerContactWithDetailsDto, ICustomerContactRepository>,
        ICustomerContactsAppService
    {
        private readonly ICustomerContactRepository _customerContactRepository;
        private readonly IDistributedCache<CustomerContactExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly CustomerContactManager _customerContactManager;

        private readonly ICustomerRepository _customerRepository;

        public CustomerContactsAppService(ICurrentTenant currentTenant,
            ICustomerContactRepository repository,
            CustomerContactManager customerContactManager,
            IConfiguration settingProvider,
            ICustomerRepository customerRepository,
            IDistributedCache<CustomerContactExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _customerContactRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerContactManager = customerContactManager;

            _customerRepository = customerRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerContactRepository", _customerContactRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerRepository", _customerRepository));
        }
    }
}
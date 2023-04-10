using DMSpro.OMS.MdmService.CustomerAttributes;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.Partial;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.CustomerGroupAttributes;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{

    [Authorize(MdmServicePermissions.CustomerAttributes.Default)]
    public partial class CustomerAttributeValuesAppService : PartialAppService<CustomerAttributeValue, CustomerAttributeValueWithDetailsDto, ICustomerAttributeValueRepository>,
        ICustomerAttributeValuesAppService
    {
        private readonly ICustomerAttributeValueRepository _customerAttributeValueRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerGroupAttributeRepository _customerGroupAttributeRepository;

        private readonly ICustomerAttributeRepository _customerAttributeRepository;

        public CustomerAttributeValuesAppService(ICurrentTenant currentTenant,
            ICustomerAttributeValueRepository repository,
            ICustomerRepository customerRepository,
            ICustomerGroupAttributeRepository customerGroupAttributeRepository,
            IConfiguration settingProvider,
            ICustomerAttributeRepository customerAttributeRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerAttributes.Default)
        {
            _customerAttributeValueRepository = repository;
            _customerRepository = customerRepository;
            _customerGroupAttributeRepository = customerGroupAttributeRepository;

            _customerAttributeRepository = customerAttributeRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICusAttributeValueRepository", _customerAttributeValueRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerAttributeRepository", _customerAttributeRepository));
        }
    }
}
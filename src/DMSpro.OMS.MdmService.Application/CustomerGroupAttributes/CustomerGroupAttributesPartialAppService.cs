using DMSpro.OMS.MdmService.CustomerGroups;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.Partial;
using System.Collections.Generic;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.CustomerAttributeValues;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public partial class CustomerGroupAttributesAppService : PartialAppService<CustomerGroupAttribute, CustomerGroupAttributeWithDetailsDto, ICustomerGroupAttributeRepository>,
        ICustomerGroupAttributesAppService
    {
        private readonly ICustomerGroupAttributeRepository _customerGroupAttributeRepository;
        private readonly CustomerGroupAttributeManager _customerGroupAttributeManager;

        private readonly ICustomerGroupRepository _customerGroupRepository;
        private readonly ICustomerAttributeValueRepository _customerAttributeValueRepository;

        public CustomerGroupAttributesAppService(ICurrentTenant currentTenant,
            ICustomerGroupAttributeRepository repository,
            CustomerGroupAttributeManager customerGroupByAttManager,
            IConfiguration settingProvider,
            ICustomerGroupRepository customerGroupRepository,
            ICustomerAttributeValueRepository customerAttributeValueRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerGroups.Default)
        {
            _customerGroupAttributeRepository = repository;
            _customerGroupAttributeManager = customerGroupByAttManager;

            _customerGroupRepository = customerGroupRepository;
            _customerAttributeValueRepository = customerAttributeValueRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupByAttRepository", _customerGroupAttributeRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupRepository", _customerGroupRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerAttributeValueRepository", _customerAttributeValueRepository));
        }
    }
}
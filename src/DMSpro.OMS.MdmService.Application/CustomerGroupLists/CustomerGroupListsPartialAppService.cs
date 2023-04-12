using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Customers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.Partial;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public partial class CustomerGroupListsAppService : PartialAppService<CustomerGroupList, CustomerGroupListWithDetailsDto, ICustomerGroupListRepository>,
        ICustomerGroupListsAppService
    {
        private readonly ICustomerGroupListRepository _customerGroupListRepository;
        private readonly CustomerGroupListManager _customerGroupListManager;

        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerGroupRepository _customerGroupRepository;

        public CustomerGroupListsAppService(ICurrentTenant currentTenant,
            ICustomerGroupListRepository repository,
            CustomerGroupListManager customerGroupListManager,
            IConfiguration settingProvider,
            ICustomerRepository customerRepository,
            ICustomerGroupRepository customerGroupRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerGroups.Default)
        {
            _customerGroupListRepository = repository;
            _customerGroupListManager = customerGroupListManager;

            _customerRepository = customerRepository;
            _customerGroupRepository = customerGroupRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupListRepository", _customerGroupListRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerRepository", _customerRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupRepository", _customerGroupRepository));
        }
    }
}
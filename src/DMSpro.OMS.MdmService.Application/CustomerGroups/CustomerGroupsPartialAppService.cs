using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.CustomerGroupAttributes;
using DMSpro.OMS.MdmService.CustomerGroupLists;
using DMSpro.OMS.MdmService.CustomerGroupGeos;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
	[Authorize(MdmServicePermissions.CustomerGroups.Default)]
	public partial class CustomerGroupsAppService : PartialAppService<CustomerGroup, CustomerGroupDto, ICustomerGroupRepository>,
		ICustomerGroupsAppService
	{
		private readonly ICustomerGroupRepository _customerGroupRepository;
        private readonly ICustomerGroupAttributeRepository _customerGroupAttributeRepository;
        private readonly ICustomerGroupListRepository _customerGroupListRepository;
        private readonly ICustomerGroupGeoRepository _customerGroupGeoRepository;
        private readonly CustomerGroupManager _customerGroupManager;

		public CustomerGroupsAppService(ICurrentTenant currentTenant,
			ICustomerGroupRepository repository,
			ICustomerGroupAttributeRepository customerGroupAttributeRepository,
			ICustomerGroupListRepository customerGroupListRepository,
			ICustomerGroupGeoRepository customerGroupGeoRepository,
			CustomerGroupManager customerGroupManager,
			IConfiguration settingProvider)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerGroups.Default)
		{
			_customerGroupRepository = repository;
            _customerGroupAttributeRepository = customerGroupAttributeRepository;
            _customerGroupListRepository = customerGroupListRepository;
            _customerGroupGeoRepository = customerGroupGeoRepository;
            _customerGroupManager = customerGroupManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupRepository", _customerGroupRepository));
		}
    }
}
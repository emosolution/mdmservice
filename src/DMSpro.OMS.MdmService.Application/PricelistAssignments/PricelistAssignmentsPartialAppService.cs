using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.CustomerGroups;
using System.Threading.Tasks;
using System;
using DMSpro.OMS.MdmService.CustomerGroupAttributes;
using DMSpro.OMS.MdmService.CustomerGroupLists;
using DMSpro.OMS.MdmService.CustomerGroupGeos;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
	[Authorize(MdmServicePermissions.PriceListAssignments.Default)]
	public partial class PricelistAssignmentsAppService : PartialAppService<PricelistAssignment, PricelistAssignmentWithDetailsDto, IPricelistAssignmentRepository>,
		IPricelistAssignmentsAppService
	{
		private readonly IPricelistAssignmentRepository _pricelistAssignmentRepository;

		private readonly IPriceListRepository _priceListRepository;
		private readonly ICustomerGroupRepository _customerGroupRepository;
        private readonly ICustomerGroupAttributeRepository _customerGroupAttributeRepository;
        private readonly ICustomerGroupListRepository _customerGroupListRepository;
        private readonly ICustomerGroupGeoRepository _customerGroupGeoRepository;
        private readonly ICustomerRepository _customerRepository;

        public PricelistAssignmentsAppService(ICurrentTenant currentTenant,
			IPricelistAssignmentRepository repository,
			IConfiguration settingProvider,
			IPriceListRepository priceListRepository,
			ICustomerGroupRepository customerGroupRepository,
			ICustomerGroupAttributeRepository customerGroupAttributeRepository,
			ICustomerGroupListRepository customerGroupListRepository,
			ICustomerGroupGeoRepository customerGroupGeoRepository,
			ICustomerRepository customerRepository)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.PriceListAssignments.Default)
		{
			_pricelistAssignmentRepository = repository;

			_priceListRepository = priceListRepository;
			_customerGroupRepository = customerGroupRepository;
            _customerGroupAttributeRepository = customerGroupAttributeRepository;
            _customerGroupListRepository = customerGroupListRepository;
            _customerGroupGeoRepository = customerGroupGeoRepository;
            _customerRepository = customerRepository;
            _repositories.AddIfNotContains(
				new KeyValuePair<string, object>("IPricelistAssignmentRepository", _pricelistAssignmentRepository));
			_repositories.AddIfNotContains(
				new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
			_repositories.AddIfNotContains(
					new KeyValuePair<string, object>("ICustomerGroupRepository", _customerGroupRepository));
		} 
    }
}
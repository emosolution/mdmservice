using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.CustomerGroups;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
	[Authorize(MdmServicePermissions.CustomerGroupByLists.Default)]
	public partial class CustomerGroupByListsAppService : PartialAppService<CustomerGroupByList, CustomerGroupByListWithDetailsDto, ICustomerGroupByListRepository>,
		ICustomerGroupByListsAppService
	{
		private readonly ICustomerGroupByListRepository _customerGroupByListRepository;
		private readonly IDistributedCache<CustomerGroupByListExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly CustomerGroupByListManager _customerGroupByListManager;

		private readonly ICustomerRepository _customerRepository;
		private readonly ICustomerGroupRepository _customerGroupRepository;

		public CustomerGroupByListsAppService(ICurrentTenant currentTenant,
			ICustomerGroupByListRepository repository,
			CustomerGroupByListManager customerGroupByListManager,
			IConfiguration settingProvider,
			ICustomerRepository customerRepository,
			ICustomerGroupRepository customerGroupRepository,
			IDistributedCache<CustomerGroupByListExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerGroupByLists.Default)
		{
			_customerGroupByListRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_customerGroupByListManager = customerGroupByListManager;
			
			_customerRepository= customerRepository;
			_customerGroupRepository= customerGroupRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupByListRepository", _customerGroupByListRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerRepository", _customerRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupRepository", _customerGroupRepository));
        }
    }
}
using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
	[Authorize(MdmServicePermissions.CustomerGroups.Default)]
	public partial class CustomerGroupsAppService : PartialAppService<CustomerGroup, CustomerGroupDto, ICustomerGroupRepository>,
		ICustomerGroupsAppService
	{
		private readonly ICustomerGroupRepository _customerGroupRepository;
		private readonly IDistributedCache<CustomerGroupExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly CustomerGroupManager _customerGroupManager;

		public CustomerGroupsAppService(ICurrentTenant currentTenant,
			ICustomerGroupRepository repository,
			CustomerGroupManager customerGroupManager,
			IConfiguration settingProvider,
			IDistributedCache<CustomerGroupExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerGroups.Default)
		{
			_customerGroupRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_customerGroupManager = customerGroupManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupRepository", _customerGroupRepository));
		}
    }
}
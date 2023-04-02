using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
	[Authorize(MdmServicePermissions.CustomerAttributes.Default)]
	public partial class CustomerAttributesAppService : PartialAppService<CustomerAttribute, CustomerAttributeDto, ICustomerAttributeRepository>,
		ICustomerAttributesAppService
	{
		private readonly ICustomerAttributeRepository _customerAttributeRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IDistributedCache<CustomerAttributeExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly CustomerAttributeManager _customerAttributeManager;

		public CustomerAttributesAppService(ICurrentTenant currentTenant,
			ICustomerAttributeRepository repository,
			ICustomerRepository customerRepository,
			CustomerAttributeManager customerAttributeManager,
			IConfiguration settingProvider,
			IDistributedCache<CustomerAttributeExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerAttributes.Default)
		{
			_customerAttributeRepository = repository;
			_customerRepository = customerRepository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_customerAttributeManager = customerAttributeManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerAttributeRepository", _customerAttributeRepository));
		}
    }
}
using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.CustomerAttributeValues;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
	[Authorize(MdmServicePermissions.CustomerAttributes.Default)]
	public partial class CustomerAttributesAppService : PartialAppService<CustomerAttribute, CustomerAttributeDto, ICustomerAttributeRepository>,
		ICustomerAttributesAppService
	{
		private readonly ICustomerAttributeRepository _customerAttributeRepository;
		private readonly ICustomerAttributeValueRepository _customerAttributeValueRepository;

		public CustomerAttributesAppService(ICurrentTenant currentTenant,
			ICustomerAttributeRepository repository,
			ICustomerAttributeValueRepository customerAttributeValueRepository,
			IConfiguration settingProvider)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerAttributes.Default)
		{
			_customerAttributeRepository = repository;
			_customerAttributeValueRepository = customerAttributeValueRepository;


            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerAttributeRepository", _customerAttributeRepository));
		}
    }
}
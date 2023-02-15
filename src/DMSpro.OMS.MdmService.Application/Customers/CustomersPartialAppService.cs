using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService.Customers
{
	[Authorize(MdmServicePermissions.Customers.Default)]
	public partial class CustomersAppService : PartialAppService<Customer, CustomerDto, ICustomerRepository>,
		ICustomersAppService
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IDistributedCache<CustomerExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly CustomerManager _customerManager;

		private readonly ISystemDataRepository _systemDataRepository;
		private readonly IPriceListRepository _priceListRepository;
		private readonly IGeoMasterRepository _geoMasterRepository;
		private readonly ICusAttributeValueRepository _cusAttributeValueRepository;
		private readonly ICompanyRepository _companyRepository;

		public CustomersAppService(ICurrentTenant currentTenant,
			ICustomerRepository repository,
			CustomerManager customerManager,
			IConfiguration settingProvider,
			ISystemDataRepository systemDataRepository,
			IPriceListRepository priceListRepository,
			IGeoMasterRepository geoMasterRepository,
            ICusAttributeValueRepository cusAttributeValueRepository,
			ICompanyRepository companyRepository,
			IDistributedCache<CustomerExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_customerRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_customerManager = customerManager;
			
			_systemDataRepository= systemDataRepository;
			_priceListRepository = priceListRepository;
			_geoMasterRepository = geoMasterRepository;
			_cusAttributeValueRepository = cusAttributeValueRepository;
			_companyRepository = companyRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISystemDataRepository", _systemDataRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IGeoMasterRepository", _geoMasterRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICusAttributeValueRepository", _cusAttributeValueRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
        }
    }
}
using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.CustomerGroups;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
	[Authorize(MdmServicePermissions.CustomerGroupByGeos.Default)]
	public partial class CustomerGroupByGeosAppService : PartialAppService<CustomerGroupByGeo, CustomerGroupByGeoWithDetailsDto, ICustomerGroupByGeoRepository>,
		ICustomerGroupByGeosAppService
	{
		private readonly ICustomerGroupByGeoRepository _customerGroupByGeoRepository;
		private readonly IDistributedCache<CustomerGroupByGeoExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly CustomerGroupByGeoManager _customerGroupByGeoManager;

		private readonly IGeoMasterRepository _geoMasterRepository;
		private readonly ICustomerGroupRepository _customerGroupRepository;

		public CustomerGroupByGeosAppService(ICurrentTenant currentTenant,
			ICustomerGroupByGeoRepository repository,
			CustomerGroupByGeoManager customerGroupByGeoManager,
			IConfiguration settingProvider,
			IGeoMasterRepository geoMasterRepository,
			ICustomerGroupRepository customerGroupRepository,
			IDistributedCache<CustomerGroupByGeoExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerGroupByGeos.Default)
		{
			_customerGroupByGeoRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_customerGroupByGeoManager = customerGroupByGeoManager;
			
			_geoMasterRepository= geoMasterRepository;
			_customerGroupRepository= customerGroupRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupByGeoRepository", _customerGroupByGeoRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IGeoMasterRepository", _geoMasterRepository));
            _repositories.AddIfNotContains(
                    new KeyValuePair<string, object>("ICustomerGroupRepository", _customerGroupRepository));
        }
    }
}
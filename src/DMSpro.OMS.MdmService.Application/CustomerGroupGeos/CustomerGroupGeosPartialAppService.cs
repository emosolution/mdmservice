using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.CustomerGroups;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.Partial;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public partial class CustomerGroupGeosAppService : PartialAppService<CustomerGroupGeo, CustomerGroupGeoWithDetailsDto, ICustomerGroupGeoRepository>,
        ICustomerGroupGeosAppService
    {
        private readonly ICustomerGroupGeoRepository _customerGroupGeoRepository;
        private readonly CustomerGroupGeoManager _customerGroupGeoManager;

        private readonly IGeoMasterRepository _geoMasterRepository;
        private readonly ICustomerGroupRepository _customerGroupRepository;

        public CustomerGroupGeosAppService(ICurrentTenant currentTenant,
            ICustomerGroupGeoRepository repository,
            CustomerGroupGeoManager customerGroupGeoManager,
            IConfiguration settingProvider,
            IGeoMasterRepository geoMasterRepository,
            ICustomerGroupRepository customerGroupRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerGroups.Default)
        {
            _customerGroupGeoRepository = repository;
            _customerGroupGeoManager = customerGroupGeoManager;

            _geoMasterRepository = geoMasterRepository;
            _customerGroupRepository = customerGroupRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupGeoRepository", _customerGroupGeoRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IGeoMasterRepository", _geoMasterRepository));
            _repositories.AddIfNotContains(
                    new KeyValuePair<string, object>("ICustomerGroupRepository", _customerGroupRepository));
        }
    }
}
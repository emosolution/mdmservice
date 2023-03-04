using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public class CustomerGroupByGeoManager : DomainService
    {
        private readonly ICustomerGroupByGeoRepository _customerGroupByGeoRepository;

        public CustomerGroupByGeoManager(ICustomerGroupByGeoRepository customerGroupByGeoRepository)
        {
            _customerGroupByGeoRepository = customerGroupByGeoRepository;
        }

        public async Task<CustomerGroupByGeo> CreateAsync(
        Guid customerGroupId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, bool active, DateTime? effectiveDate = null)
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));

            var customerGroupByGeo = new CustomerGroupByGeo(
             GuidGenerator.Create(),
             customerGroupId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id, active, effectiveDate
             );

            return await _customerGroupByGeoRepository.InsertAsync(customerGroupByGeo);
        }

        public async Task<CustomerGroupByGeo> UpdateAsync(
            Guid id,
            Guid customerGroupId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, bool active, DateTime? effectiveDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));

            var customerGroupByGeo = await _customerGroupByGeoRepository.GetAsync(id);

            customerGroupByGeo.CustomerGroupId = customerGroupId;
            customerGroupByGeo.GeoMaster0Id = geoMaster0Id;
            customerGroupByGeo.GeoMaster1Id = geoMaster1Id;
            customerGroupByGeo.GeoMaster2Id = geoMaster2Id;
            customerGroupByGeo.GeoMaster3Id = geoMaster3Id;
            customerGroupByGeo.GeoMaster4Id = geoMaster4Id;
            customerGroupByGeo.Active = active;
            customerGroupByGeo.EffectiveDate = effectiveDate;

            customerGroupByGeo.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerGroupByGeoRepository.UpdateAsync(customerGroupByGeo);
        }

    }
}
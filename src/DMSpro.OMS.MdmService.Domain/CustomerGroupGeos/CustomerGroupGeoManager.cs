using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeoManager : DomainService
    {
        private readonly ICustomerGroupGeoRepository _customerGroupGeoRepository;

        public CustomerGroupGeoManager(ICustomerGroupGeoRepository customerGroupGeoRepository)
        {
            _customerGroupGeoRepository = customerGroupGeoRepository;
        }

        public async Task<CustomerGroupGeo> CreateAsync(
        Guid customerGroupId, Guid geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, string description, bool active)
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));
            Check.NotNull(geoMaster0Id, nameof(geoMaster0Id));
            Check.Length(description, nameof(description), CustomerGroupGeoConsts.DescriptionMaxLength);

            var customerGroupGeo = new CustomerGroupGeo(
             GuidGenerator.Create(),
             customerGroupId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id, description, active
             );

            return await _customerGroupGeoRepository.InsertAsync(customerGroupGeo);
        }

        public async Task<CustomerGroupGeo> UpdateAsync(
            Guid id,
            Guid customerGroupId, Guid geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, string description, bool active, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));
            Check.NotNull(geoMaster0Id, nameof(geoMaster0Id));
            Check.Length(description, nameof(description), CustomerGroupGeoConsts.DescriptionMaxLength);

            var customerGroupGeo = await _customerGroupGeoRepository.GetAsync(id);

            customerGroupGeo.CustomerGroupId = customerGroupId;
            customerGroupGeo.GeoMaster0Id = geoMaster0Id;
            customerGroupGeo.GeoMaster1Id = geoMaster1Id;
            customerGroupGeo.GeoMaster2Id = geoMaster2Id;
            customerGroupGeo.GeoMaster3Id = geoMaster3Id;
            customerGroupGeo.GeoMaster4Id = geoMaster4Id;
            customerGroupGeo.Description = description;
            customerGroupGeo.Active = active;

            customerGroupGeo.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerGroupGeoRepository.UpdateAsync(customerGroupGeo);
        }

    }
}
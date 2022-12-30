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
        Guid customerGroupId, Guid geoMasterId, bool active, DateTime? effectiveDate = null)
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));
            Check.NotNull(geoMasterId, nameof(geoMasterId));

            var customerGroupByGeo = new CustomerGroupByGeo(
             GuidGenerator.Create(),
             customerGroupId, geoMasterId, active, effectiveDate
             );

            return await _customerGroupByGeoRepository.InsertAsync(customerGroupByGeo);
        }

        public async Task<CustomerGroupByGeo> UpdateAsync(
            Guid id,
            Guid customerGroupId, Guid geoMasterId, bool active, DateTime? effectiveDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));
            Check.NotNull(geoMasterId, nameof(geoMasterId));

            var queryable = await _customerGroupByGeoRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var customerGroupByGeo = await AsyncExecuter.FirstOrDefaultAsync(query);

            customerGroupByGeo.CustomerGroupId = customerGroupId;
            customerGroupByGeo.GeoMasterId = geoMasterId;
            customerGroupByGeo.Active = active;
            customerGroupByGeo.EffectiveDate = effectiveDate;

            customerGroupByGeo.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerGroupByGeoRepository.UpdateAsync(customerGroupByGeo);
        }

    }
}
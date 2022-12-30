using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public class CustomerInZoneManager : DomainService
    {
        private readonly ICustomerInZoneRepository _customerInZoneRepository;

        public CustomerInZoneManager(ICustomerInZoneRepository customerInZoneRepository)
        {
            _customerInZoneRepository = customerInZoneRepository;
        }

        public async Task<CustomerInZone> CreateAsync(
        Guid salesOrgHierarchyId, Guid customerId, DateTime? effectiveDate = null, DateTime? endDate = null)
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(customerId, nameof(customerId));

            var customerInZone = new CustomerInZone(
             GuidGenerator.Create(),
             salesOrgHierarchyId, customerId, effectiveDate, endDate
             );

            return await _customerInZoneRepository.InsertAsync(customerInZone);
        }

        public async Task<CustomerInZone> UpdateAsync(
            Guid id,
            Guid salesOrgHierarchyId, Guid customerId, DateTime? effectiveDate = null, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(customerId, nameof(customerId));

            var queryable = await _customerInZoneRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var customerInZone = await AsyncExecuter.FirstOrDefaultAsync(query);

            customerInZone.SalesOrgHierarchyId = salesOrgHierarchyId;
            customerInZone.CustomerId = customerId;
            customerInZone.EffectiveDate = effectiveDate;
            customerInZone.EndDate = endDate;

            customerInZone.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerInZoneRepository.UpdateAsync(customerInZone);
        }

    }
}
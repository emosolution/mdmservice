using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
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
        Guid salesOrgHierarchyId, Guid customerId, DateTime effectiveDate, DateTime? endDate = null)
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var customerInZone = new CustomerInZone(
             GuidGenerator.Create(),
             salesOrgHierarchyId, customerId, effectiveDate, endDate
             );

            return await _customerInZoneRepository.InsertAsync(customerInZone);
        }

        public async Task<CustomerInZone> UpdateAsync(
            Guid id,
            Guid salesOrgHierarchyId, Guid customerId, DateTime effectiveDate, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var customerInZone = await _customerInZoneRepository.GetAsync(id);

            customerInZone.SalesOrgHierarchyId = salesOrgHierarchyId;
            customerInZone.CustomerId = customerId;
            customerInZone.EffectiveDate = effectiveDate;
            customerInZone.EndDate = endDate;

            customerInZone.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerInZoneRepository.UpdateAsync(customerInZone);
        }

    }
}
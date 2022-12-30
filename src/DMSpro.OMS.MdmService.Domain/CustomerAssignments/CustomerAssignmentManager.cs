using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
    public class CustomerAssignmentManager : DomainService
    {
        private readonly ICustomerAssignmentRepository _customerAssignmentRepository;

        public CustomerAssignmentManager(ICustomerAssignmentRepository customerAssignmentRepository)
        {
            _customerAssignmentRepository = customerAssignmentRepository;
        }

        public async Task<CustomerAssignment> CreateAsync(
        Guid companyId, Guid customerId, DateTime effectiveDate, DateTime? endDate = null)
        {
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var customerAssignment = new CustomerAssignment(
             GuidGenerator.Create(),
             companyId, customerId, effectiveDate, endDate
             );

            return await _customerAssignmentRepository.InsertAsync(customerAssignment);
        }

        public async Task<CustomerAssignment> UpdateAsync(
            Guid id,
            Guid companyId, Guid customerId, DateTime effectiveDate, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var queryable = await _customerAssignmentRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var customerAssignment = await AsyncExecuter.FirstOrDefaultAsync(query);

            customerAssignment.CompanyId = companyId;
            customerAssignment.CustomerId = customerId;
            customerAssignment.EffectiveDate = effectiveDate;
            customerAssignment.EndDate = endDate;

            customerAssignment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerAssignmentRepository.UpdateAsync(customerAssignment);
        }

    }
}
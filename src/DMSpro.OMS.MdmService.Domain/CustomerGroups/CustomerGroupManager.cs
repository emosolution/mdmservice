using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupManager : DomainService
    {
        private readonly ICustomerGroupRepository _customerGroupRepository;

        public CustomerGroupManager(ICustomerGroupRepository customerGroupRepository)
        {
            _customerGroupRepository = customerGroupRepository;
        }

        public async Task<CustomerGroup> CreateAsync(
        string code, string name, bool active, Type groupBy, Status status, DateTime? effectiveDate = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CustomerGroupConsts.CodeMaxLength, CustomerGroupConsts.CodeMinLength);
            Check.NotNull(groupBy, nameof(groupBy));
            Check.NotNull(status, nameof(status));

            var customerGroup = new CustomerGroup(
             GuidGenerator.Create(),
             code, name, active, groupBy, status, effectiveDate
             );

            return await _customerGroupRepository.InsertAsync(customerGroup);
        }

        public async Task<CustomerGroup> UpdateAsync(
            Guid id,
            string code, string name, bool active, Type groupBy, Status status, DateTime? effectiveDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CustomerGroupConsts.CodeMaxLength, CustomerGroupConsts.CodeMinLength);
            Check.NotNull(groupBy, nameof(groupBy));
            Check.NotNull(status, nameof(status));

            var queryable = await _customerGroupRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var customerGroup = await AsyncExecuter.FirstOrDefaultAsync(query);

            customerGroup.Code = code;
            customerGroup.Name = name;
            customerGroup.Active = active;
            customerGroup.GroupBy = groupBy;
            customerGroup.Status = status;
            customerGroup.EffectiveDate = effectiveDate;

            customerGroup.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerGroupRepository.UpdateAsync(customerGroup);
        }

    }
}
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
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
        string code, string name, bool selectable, Type groupBy, string description)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CustomerGroupConsts.CodeMaxLength, CustomerGroupConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), CustomerGroupConsts.NameMaxLength, CustomerGroupConsts.NameMinLength);
            Check.NotNull(groupBy, nameof(groupBy));
            Check.Length(description, nameof(description), CustomerGroupConsts.DescriptionMaxLength);

            var customerGroup = new CustomerGroup(
                GuidGenerator.Create(),
                code, name, selectable, groupBy, Status.OPEN, description);

            return await _customerGroupRepository.InsertAsync(customerGroup);
        }

        public async Task<CustomerGroup> UpdateAsync(
            Guid id,
            string name, bool selectable, Type groupBy, string description, 
            [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), CustomerGroupConsts.NameMaxLength, CustomerGroupConsts.NameMinLength);
            Check.NotNull(groupBy, nameof(groupBy));
            Check.Length(description, nameof(description), CustomerGroupConsts.DescriptionMaxLength);

            var customerGroup = await _customerGroupRepository.GetAsync(id);

            customerGroup.Name = name;
            customerGroup.Selectable = selectable;
            customerGroup.GroupBy = groupBy;
            customerGroup.Description = description;

            customerGroup.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerGroupRepository.UpdateAsync(customerGroup);
        }

    }
}
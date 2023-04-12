using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListManager : DomainService
    {
        private readonly ICustomerGroupListRepository _customerGroupListRepository;

        public CustomerGroupListManager(ICustomerGroupListRepository customerGroupListRepository)
        {
            _customerGroupListRepository = customerGroupListRepository;
        }

        public async Task<CustomerGroupList> CreateAsync(
            Guid customerId, Guid customerGroupId, string description, bool active)
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(customerGroupId, nameof(customerGroupId));

            var customerGroupList = new CustomerGroupList(
                GuidGenerator.Create(),
                customerId, customerGroupId, description, active);

            return await _customerGroupListRepository.InsertAsync(customerGroupList);
        }

        public async Task<CustomerGroupList> UpdateAsync(
            Guid id,
            Guid customerId,
            [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerId, nameof(customerId));

            var customerGroupList = await _customerGroupListRepository.GetAsync(id);

            customerGroupList.CustomerId = customerId;

            customerGroupList.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerGroupListRepository.UpdateAsync(customerGroupList);
        }

    }
}
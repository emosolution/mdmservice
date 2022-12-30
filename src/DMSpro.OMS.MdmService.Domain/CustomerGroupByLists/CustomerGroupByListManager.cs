using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class CustomerGroupByListManager : DomainService
    {
        private readonly ICustomerGroupByListRepository _customerGroupByListRepository;

        public CustomerGroupByListManager(ICustomerGroupByListRepository customerGroupByListRepository)
        {
            _customerGroupByListRepository = customerGroupByListRepository;
        }

        public async Task<CustomerGroupByList> CreateAsync(
        Guid customerGroupId, Guid customerId, bool active)
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));
            Check.NotNull(customerId, nameof(customerId));

            var customerGroupByList = new CustomerGroupByList(
             GuidGenerator.Create(),
             customerGroupId, customerId, active
             );

            return await _customerGroupByListRepository.InsertAsync(customerGroupByList);
        }

        public async Task<CustomerGroupByList> UpdateAsync(
            Guid id,
            Guid customerGroupId, Guid customerId, bool active, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));
            Check.NotNull(customerId, nameof(customerId));

            var queryable = await _customerGroupByListRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var customerGroupByList = await AsyncExecuter.FirstOrDefaultAsync(query);

            customerGroupByList.CustomerGroupId = customerGroupId;
            customerGroupByList.CustomerId = customerId;
            customerGroupByList.Active = active;

            customerGroupByList.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerGroupByListRepository.UpdateAsync(customerGroupByList);
        }

    }
}
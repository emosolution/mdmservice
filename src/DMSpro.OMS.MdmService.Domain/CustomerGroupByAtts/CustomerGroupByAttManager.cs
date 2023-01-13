using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttManager : DomainService
    {
        private readonly ICustomerGroupByAttRepository _customerGroupByAttRepository;

        public CustomerGroupByAttManager(ICustomerGroupByAttRepository customerGroupByAttRepository)
        {
            _customerGroupByAttRepository = customerGroupByAttRepository;
        }

        public async Task<CustomerGroupByAtt> CreateAsync(
        Guid customerGroupId, Guid cusAttributeValueId, string valueCode, string valueName)
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));
            Check.NotNull(cusAttributeValueId, nameof(cusAttributeValueId));

            var customerGroupByAtt = new CustomerGroupByAtt(
             GuidGenerator.Create(),
             customerGroupId, cusAttributeValueId, valueCode, valueName
             );

            return await _customerGroupByAttRepository.InsertAsync(customerGroupByAtt);
        }

        public async Task<CustomerGroupByAtt> UpdateAsync(
            Guid id,
            Guid customerGroupId, Guid cusAttributeValueId, string valueCode, string valueName, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));
            Check.NotNull(cusAttributeValueId, nameof(cusAttributeValueId));

            var queryable = await _customerGroupByAttRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var customerGroupByAtt = await AsyncExecuter.FirstOrDefaultAsync(query);

            customerGroupByAtt.CustomerGroupId = customerGroupId;
            customerGroupByAtt.CusAttributeValueId = cusAttributeValueId;
            customerGroupByAtt.ValueCode = valueCode;
            customerGroupByAtt.ValueName = valueName;

            customerGroupByAtt.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerGroupByAttRepository.UpdateAsync(customerGroupByAtt);
        }

    }
}
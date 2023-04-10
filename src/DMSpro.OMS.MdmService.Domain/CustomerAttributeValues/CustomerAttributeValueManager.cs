using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class CustomerAttributeValueManager : DomainService
    {
        private readonly ICustomerAttributeValueRepository _customerAttributeValueRepository;

        public CustomerAttributeValueManager(ICustomerAttributeValueRepository customerAttributeValueRepository)
        {
            _customerAttributeValueRepository = customerAttributeValueRepository;
        }

        public async Task<CustomerAttributeValue> CreateAsync(
        Guid customerAttributeId, string code, string attrValName)
        {
            Check.NotNull(customerAttributeId, nameof(customerAttributeId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CustomerAttributeValueConsts.CodeMaxLength, CustomerAttributeValueConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), CustomerAttributeValueConsts.AttrValNameMaxLength, CustomerAttributeValueConsts.AttrValNameMinLength);

            var customerAttributeValue = new CustomerAttributeValue(
             GuidGenerator.Create(),
             customerAttributeId, code, attrValName
             );

            return await _customerAttributeValueRepository.InsertAsync(customerAttributeValue);
        }

        public async Task<CustomerAttributeValue> UpdateAsync(
            Guid id,
            Guid customerAttributeId, string code, string attrValName, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerAttributeId, nameof(customerAttributeId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CustomerAttributeValueConsts.CodeMaxLength, CustomerAttributeValueConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), CustomerAttributeValueConsts.AttrValNameMaxLength, CustomerAttributeValueConsts.AttrValNameMinLength);

            var customerAttributeValue = await _customerAttributeValueRepository.GetAsync(id);

            customerAttributeValue.CustomerAttributeId = customerAttributeId;
            customerAttributeValue.Code = code;
            customerAttributeValue.AttrValName = attrValName;

            customerAttributeValue.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerAttributeValueRepository.UpdateAsync(customerAttributeValue);
        }

    }
}
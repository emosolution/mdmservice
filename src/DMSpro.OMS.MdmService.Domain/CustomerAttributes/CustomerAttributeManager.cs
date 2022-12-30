using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributeManager : DomainService
    {
        private readonly ICustomerAttributeRepository _customerAttributeRepository;

        public CustomerAttributeManager(ICustomerAttributeRepository customerAttributeRepository)
        {
            _customerAttributeRepository = customerAttributeRepository;
        }

        public async Task<CustomerAttribute> CreateAsync(
        int attrNo, string attrName, bool active, int? hierarchyLevel = null)
        {
            Check.Range(attrNo, nameof(attrNo), CustomerAttributeConsts.AttrNoMinLength, CustomerAttributeConsts.AttrNoMaxLength);
            Check.NotNullOrWhiteSpace(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName), CustomerAttributeConsts.AttrNameMaxLength, CustomerAttributeConsts.AttrNameMinLength);

            var customerAttribute = new CustomerAttribute(
             GuidGenerator.Create(),
             attrNo, attrName, active, hierarchyLevel
             );

            return await _customerAttributeRepository.InsertAsync(customerAttribute);
        }

        public async Task<CustomerAttribute> UpdateAsync(
            Guid id,
            int attrNo, string attrName, bool active, int? hierarchyLevel = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.Range(attrNo, nameof(attrNo), CustomerAttributeConsts.AttrNoMinLength, CustomerAttributeConsts.AttrNoMaxLength);
            Check.NotNullOrWhiteSpace(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName), CustomerAttributeConsts.AttrNameMaxLength, CustomerAttributeConsts.AttrNameMinLength);

            var queryable = await _customerAttributeRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var customerAttribute = await AsyncExecuter.FirstOrDefaultAsync(query);

            customerAttribute.AttrNo = attrNo;
            customerAttribute.AttrName = attrName;
            customerAttribute.Active = active;
            customerAttribute.HierarchyLevel = hierarchyLevel;

            customerAttribute.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerAttributeRepository.UpdateAsync(customerAttribute);
        }

    }
}
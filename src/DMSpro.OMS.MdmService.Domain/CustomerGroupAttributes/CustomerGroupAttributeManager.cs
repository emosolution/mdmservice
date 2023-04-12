using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public class CustomerGroupAttributeManager : DomainService
    {
        private readonly ICustomerGroupAttributeRepository _customerGroupAttributeRepository;

        public CustomerGroupAttributeManager(ICustomerGroupAttributeRepository customerGroupAttributeRepository)
        {
            _customerGroupAttributeRepository = customerGroupAttributeRepository;
        }

        public async Task<CustomerGroupAttribute> CreateAsync(
            Guid customerGroupId, 
            Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, 
            Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, 
            Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, 
            Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, 
            string description)
        {
            Check.NotNull(customerGroupId, nameof(customerGroupId));
            Check.Length(description, nameof(description), CustomerGroupAttributeConsts.DescriptionMaxLength);

            var customerGroupAttribute = new CustomerGroupAttribute(
                GuidGenerator.Create(), customerGroupId, 
                attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, 
                attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, 
                attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, 
                attr15Id, attr16Id, attr17Id, attr18Id, attr19Id, 
                description);

            return await _customerGroupAttributeRepository.InsertAsync(customerGroupAttribute);
        }

        public async Task<CustomerGroupAttribute> UpdateAsync(
            Guid id,
            Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, 
            Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, 
            Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, 
            Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, 
            string description, 
            [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.Length(description, nameof(description), CustomerGroupAttributeConsts.DescriptionMaxLength);

            var customerGroupAttribute = await _customerGroupAttributeRepository.GetAsync(id);

            customerGroupAttribute.Attr0Id = attr0Id;
            customerGroupAttribute.Attr1Id = attr1Id;
            customerGroupAttribute.Attr2Id = attr2Id;
            customerGroupAttribute.Attr3Id = attr3Id;
            customerGroupAttribute.Attr4Id = attr4Id;
            customerGroupAttribute.Attr5Id = attr5Id;
            customerGroupAttribute.Attr6Id = attr6Id;
            customerGroupAttribute.Attr7Id = attr7Id;
            customerGroupAttribute.Attr8Id = attr8Id;
            customerGroupAttribute.Attr9Id = attr9Id;
            customerGroupAttribute.Attr10Id = attr10Id;
            customerGroupAttribute.Attr11Id = attr11Id;
            customerGroupAttribute.Attr12Id = attr12Id;
            customerGroupAttribute.Attr13Id = attr13Id;
            customerGroupAttribute.Attr14Id = attr14Id;
            customerGroupAttribute.Attr15Id = attr15Id;
            customerGroupAttribute.Attr16Id = attr16Id;
            customerGroupAttribute.Attr17Id = attr17Id;
            customerGroupAttribute.Attr18Id = attr18Id;
            customerGroupAttribute.Attr19Id = attr19Id;
            customerGroupAttribute.Description = description;

            customerGroupAttribute.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerGroupAttributeRepository.UpdateAsync(customerGroupAttribute);
        }

    }
}
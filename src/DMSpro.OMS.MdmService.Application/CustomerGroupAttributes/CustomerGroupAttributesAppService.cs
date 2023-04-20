using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.CustomerGroups;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public partial class CustomerGroupAttributesAppService
    { 
        [Authorize(MdmServicePermissions.CustomerGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var customerGroupAttribute = await _customerGroupAttributeRepository.GetAsync(id);
            await CheckCustomerGroup(customerGroupAttribute.CustomerGroupId);
            await _customerGroupAttributeRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Create)]
        public virtual async Task<CustomerGroupAttributeDto> CreateAsync(CustomerGroupAttributeCreateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            await CheckCustomerGroup(input.CustomerGroupId);
            await CheckAllAttributeValues(
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id,
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id,
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id,
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id);
            var customerGroupAttribute = await _customerGroupAttributeManager.CreateAsync(
                input.CustomerGroupId, 
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, 
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, 
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, 
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, 
                input.Description
            );

            return ObjectMapper.Map<CustomerGroupAttribute, CustomerGroupAttributeDto>(customerGroupAttribute);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Edit)]
        public virtual async Task<CustomerGroupAttributeDto> UpdateAsync(Guid id, CustomerGroupAttributeUpdateDto input)
        {
            var customerGroupAttribute = await _customerGroupAttributeRepository.GetAsync(id);
            await CheckCustomerGroup(customerGroupAttribute.CustomerGroupId);
            await CheckAllAttributeValues(
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id,
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id,
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id,
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id);

            await _customerGroupAttributeManager.UpdateAsync(
                id,
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, 
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, 
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, 
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, 
                input.Description, 
                input.ConcurrencyStamp);

            return ObjectMapper.Map<CustomerGroupAttribute, CustomerGroupAttributeDto>(customerGroupAttribute);
        }

        public async Task<CustomerGroupAttributeDto> GetAsync(Guid id)
        {
            var record = await _customerGroupAttributeRepository.GetAsync(x => x.Id == id);
            return ObjectMapper.Map<CustomerGroupAttribute, CustomerGroupAttributeDto>(record);
        }

        private async Task CheckCustomerGroup(Guid customerGroupId)
        {
            var customerGroup = await _customerGroupRepository.GetAsync(customerGroupId);
            if (customerGroup.Status != Status.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupAttributesAppService:550"], code: "1");
            }
            if (customerGroup.GroupBy != CustomerGroups.Type.ATTRIBUTE)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupAttributesAppService:551"], code: "1");
            }
        }

        private async Task CheckAllAttributeValues(
            Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id,
            Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id,
            Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id,
            Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id)
        {

            if (attr0Id == null && attr1Id == null && attr2Id == null && attr3Id == null && attr4Id == null &&
                attr5Id == null && attr6Id == null && attr7Id == null && attr8Id == null && attr9Id == null &&
                attr10Id == null && attr11Id == null && attr12Id == null && attr13Id == null && attr14Id == null &&
                attr15Id == null && attr16Id == null && attr17Id == null && attr18Id == null && attr19Id == null)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupAttributesAppService:554"], code: "0");
            }

            List<(Guid?, int)> checkInputs = new()
            {
                (attr0Id, 1), (attr1Id, 2), (attr2Id, 3),(attr3Id, 4),(attr4Id, 5),
                (attr5Id, 6),(attr6Id, 7),(attr7Id, 8),(attr8Id, 9),(attr9Id, 10),
                (attr10Id, 11),(attr11Id, 12),(attr12Id, 13),(attr13Id, 14),(attr14Id, 15),
                (attr15Id, 16),(attr16Id, 17),(attr17Id, 18),(attr18Id, 19),(attr19Id, 120),
            };
            foreach (var input in checkInputs)
            {
                if (input.Item1 == null)
                {
                    continue;
                }
                await CheckAttributeValue((Guid)input.Item1, input.Item2);
            }
        }

        private async Task CheckAttributeValue(Guid attributeValueId, int attrNo)
        {
            var customerAttribute = await _customerAttributeRepository.GetAsync(x => x.AttrNo == attrNo - 1);
            if (!customerAttribute.Active)
            {
                throw new UserFriendlyException(
                    message: L["Error:CustomerGroupAttributesAppService:552", attrNo.ToString()], code: "1");
            }
            var customerAttributeValue =
                await _customerAttributeValueRepository.GetAsync(x => x.Id == attributeValueId);
            if (customerAttributeValue.CustomerAttributeId != customerAttribute.Id)
            {
                throw new UserFriendlyException(
                    message: L["Error:CustomerGroupAttributesAppService:553", attrNo.ToString()], code: "1");
            }
        }
    }
}
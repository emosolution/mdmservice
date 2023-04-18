using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{

    [Authorize(MdmServicePermissions.CustomerAttributes.Default)]
    public partial class CustomerAttributeValuesAppService
    {

        public virtual async Task<CustomerAttributeValueDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAttributeValue, CustomerAttributeValueDto>(await _customerAttributeValueRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await CheckAttributeValueUsedByCustomer(id);
            await CheckAttributeValueUsedByCustomerGroup(id);
            await _customerAttributeValueRepository.GetAsync(id);
            await _customerAttributeValueRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Create)]
        public virtual async Task<CustomerAttributeValueDto> CreateAsync(CustomerAttributeValueCreateDto input)
        {
            await CheckCodeAndName(input.Code, input.AttrValName);
            var attribute = await _customerAttributeRepository.GetAsync(input.CustomerAttributeId);
            var attributeValue = new CustomerAttributeValue(GuidGenerator.Create(),
                attribute.Id, input.Code, input.AttrValName);
            await _customerAttributeValueRepository.InsertAsync(attributeValue);
            return ObjectMapper.Map<CustomerAttributeValue, CustomerAttributeValueDto>(attributeValue);
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Edit)]
        public virtual async Task<CustomerAttributeValueDto> UpdateAsync(Guid id, CustomerAttributeValueUpdateDto input)
        {
            await CheckName(input.AttrValName, id);

            var attributeValue = await _customerAttributeValueRepository.GetAsync(id);
            attributeValue.AttrValName = input.AttrValName;
            attributeValue.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _customerAttributeValueRepository.UpdateAsync(attributeValue);

            return ObjectMapper.Map<CustomerAttributeValue, CustomerAttributeValueDto>(attributeValue);
        }

        private async Task CheckAttributeValueUsedByCustomer(Guid id)
        {
            if (await _customerRepository.AnyAsync(
                x => x.Attr0Id == id || x.Attr1Id == id ||
                x.Attr2Id == id || x.Attr3Id == id ||
                x.Attr4Id == id || x.Attr5Id == id ||
                x.Attr6Id == id || x.Attr7Id == id ||
                x.Attr8Id == id || x.Attr9Id == id ||
                x.Attr10Id == id || x.Attr11Id == id ||
                x.Attr12Id == id || x.Attr13Id == id ||
                x.Attr14Id == id || x.Attr15Id == id ||
                x.Attr16Id == id || x.Attr17Id == id ||
                x.Attr18Id == id || x.Attr19Id == id))
            {
                throw new UserFriendlyException(message: L["Error:CustomerAttributeValuesAppService:550"], code: "0");
            }
        }

        private async Task CheckAttributeValueUsedByCustomerGroup(Guid id)
        {
            if (await _customerGroupAttributeRepository.AnyAsync(
                x => x.Attr0Id == id || x.Attr1Id == id ||
                x.Attr2Id == id || x.Attr3Id == id ||
                x.Attr4Id == id || x.Attr5Id == id ||
                x.Attr6Id == id || x.Attr7Id == id ||
                x.Attr8Id == id || x.Attr9Id == id ||
                x.Attr10Id == id || x.Attr11Id == id ||
                x.Attr12Id == id || x.Attr13Id == id ||
                x.Attr14Id == id || x.Attr15Id == id ||
                x.Attr16Id == id || x.Attr17Id == id ||
                x.Attr18Id == id || x.Attr19Id == id))
            {
                throw new UserFriendlyException(message: L["Error:CustomerAttributeValuesAppService:551"], code: "0");
            }
        }

        private async Task CheckCodeAndName(string code, string attrName)
        {
            await CheckCode(code);
            await CheckName(attrName);
        }

        private async Task CheckName(string attrName, Guid? id = null)
        {
            Check.NotNullOrWhiteSpace(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName),
                CustomerAttributeValueConsts.AttrValNameMaxLength, CustomerAttributeValueConsts.AttrValNameMinLength);
            var record = await _customerAttributeValueRepository.FirstOrDefaultAsync(x => x.AttrValName == attrName);
            if (record != null && record.Id != id)
            {
                throw new UserFriendlyException(message: L["Error:CustomerAttributeValuesAppService:552"], code: "0");
            }
        }

        private async Task CheckCode(string code)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code),
                CustomerAttributeValueConsts.CodeMaxLength, CustomerAttributeValueConsts.CodeMinLength);
            if (await _customerAttributeValueRepository.AnyAsync(x => x.Code == code))
            {
                throw new UserFriendlyException(message: L["Error:CustomerAttributeValuesAppService:553"], code: "0");
            }
        }
    }
}
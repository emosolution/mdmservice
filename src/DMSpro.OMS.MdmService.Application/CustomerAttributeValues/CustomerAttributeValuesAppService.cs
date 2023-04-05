using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;

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
            await _customerAttributeValueRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Create)]
        public virtual async Task<CustomerAttributeValueDto> CreateAsync(CustomerAttributeValueCreateDto input)
        {
            if (input.CustomerAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerAttribute"]]);
            }

            var customerAttributeValue = await _customerAttributeValueManager.CreateAsync(
            input.CustomerAttributeId, input.ParentId, input.Code, input.AttrValName
            );

            return ObjectMapper.Map<CustomerAttributeValue, CustomerAttributeValueDto>(customerAttributeValue);
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Edit)]
        public virtual async Task<CustomerAttributeValueDto> UpdateAsync(Guid id, CustomerAttributeValueUpdateDto input)
        {
            if (input.CustomerAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerAttribute"]]);
            }

            var customerAttributeValue = await _customerAttributeValueManager.UpdateAsync(
            id,
            input.CustomerAttributeId, input.ParentId, input.Code, input.AttrValName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerAttributeValue, CustomerAttributeValueDto>(customerAttributeValue);
        }
    }
}
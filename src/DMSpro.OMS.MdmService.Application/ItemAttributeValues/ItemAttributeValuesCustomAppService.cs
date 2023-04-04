using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.ItemAttributes;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{

    [Authorize(MdmServicePermissions.ItemAttributeValues.Default)]
    public partial class ItemAttributeValuesAppService
    {
        public virtual async Task<ItemAttributeValueDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttributeValue, ItemAttributeValueDto>(await _itemAttributeValueRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemAttributeValues.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemAttributeValueRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemAttributeValues.Create)]
        public virtual async Task<ItemAttributeValueDto> CreateAsync(ItemAttributeValueCreateDto input)
        {
            if (input.ItemAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemAttribute"]]);
            }

            var itemAttributeValue = await _itemAttributeValueManager.CreateAsync(
            input.ItemAttributeId, input.ParentId, input.AttrValName);

            return ObjectMapper.Map<ItemAttributeValue, ItemAttributeValueDto>(itemAttributeValue);
        }

        [Authorize(MdmServicePermissions.ItemAttributeValues.Edit)]
        public virtual async Task<ItemAttributeValueDto> UpdateAsync(Guid id, ItemAttributeValueUpdateDto input)
        {
            if (input.ItemAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemAttribute"]]);
            }

            var itemAttributeValue = await _itemAttributeValueManager.UpdateAsync(
            id,
            input.ItemAttributeId, input.ParentId, input.AttrValName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemAttributeValue, ItemAttributeValueDto>(itemAttributeValue);
        }
    }
}
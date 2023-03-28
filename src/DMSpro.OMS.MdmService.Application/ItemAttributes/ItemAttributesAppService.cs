using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemAttributes
{

    [Authorize(MdmServicePermissions.ItemAttributes.Default)]
    public partial class ItemAttributesAppService
    {
        public virtual async Task<ItemAttributeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(await _itemAttributeRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemAttributeRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Create)]
        public virtual async Task<ItemAttributeDto> CreateAsync(ItemAttributeCreateDto input)
        {

            Check.Range(input.AttrNo, nameof(input.AttrNo), ItemAttributeConsts.AttrNoMinLength, ItemAttributeConsts.AttrNoMaxLength);
            Check.NotNullOrWhiteSpace(input.AttrName, nameof(input.AttrName));
            Check.Length(input.AttrName, nameof(input.AttrName), ItemAttributeConsts.AttrNameMaxLength, ItemAttributeConsts.AttrNameMinLength);

            var itemAttribute = new ItemAttribute(
                GuidGenerator.Create(),
                input.AttrNo, input.AttrName, input.Active, input.HierarchyLevel
             );

            var newRecord = await _itemAttributeRepository.InsertAsync(itemAttribute);

            return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(newRecord);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Edit)]
        public virtual async Task<ItemAttributeDto> UpdateAsync(Guid id, ItemAttributeUpdateDto input)
        {
            Check.Range(input.AttrNo, nameof(input.AttrNo), ItemAttributeConsts.AttrNoMinLength, ItemAttributeConsts.AttrNoMaxLength);
            Check.NotNullOrWhiteSpace(input.AttrName, nameof(input.AttrName));
            Check.Length(input.AttrName, nameof(input.AttrName), ItemAttributeConsts.AttrNameMaxLength, ItemAttributeConsts.AttrNameMinLength);

            var itemAttribute = await _itemAttributeRepository.GetAsync(id);

            itemAttribute.AttrNo = input.AttrNo;
            itemAttribute.AttrName = input.AttrName;
            itemAttribute.Active = input.Active;
            itemAttribute.HierarchyLevel = input.HierarchyLevel;

            itemAttribute.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            var updatedRecord = await _itemAttributeRepository.UpdateAsync(itemAttribute);

            return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(updatedRecord);
        }
    }
}
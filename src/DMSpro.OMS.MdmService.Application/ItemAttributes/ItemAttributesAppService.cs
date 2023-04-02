using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

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

            var attribute = await _itemAttributeRepository.GetAsync(id);

            if (!input.Active)
            {
                if (await _itemRepository.AnyAsync(x =>
                    (attribute.AttrNo == 0 && x.Attr0Id.HasValue) ||
                    (attribute.AttrNo == 1 && x.Attr1Id.HasValue) ||
                    (attribute.AttrNo == 2 && x.Attr2Id.HasValue) ||
                    (attribute.AttrNo == 3 && x.Attr3Id.HasValue) ||
                    (attribute.AttrNo == 4 && x.Attr4Id.HasValue) ||
                    (attribute.AttrNo == 5 && x.Attr5Id.HasValue) ||
                    (attribute.AttrNo == 6 && x.Attr6Id.HasValue) ||
                    (attribute.AttrNo == 7 && x.Attr7Id.HasValue) ||
                    (attribute.AttrNo == 8 && x.Attr8Id.HasValue) ||
                    (attribute.AttrNo == 9 && x.Attr9Id.HasValue) ||
                    (attribute.AttrNo == 10 && x.Attr10Id.HasValue) ||
                    (attribute.AttrNo == 11 && x.Attr11Id.HasValue) ||
                    (attribute.AttrNo == 12 && x.Attr12Id.HasValue) ||
                    (attribute.AttrNo == 13 && x.Attr13Id.HasValue) ||
                    (attribute.AttrNo == 14 && x.Attr14Id.HasValue) ||
                    (attribute.AttrNo == 15 && x.Attr15Id.HasValue) ||
                    (attribute.AttrNo == 16 && x.Attr16Id.HasValue) ||
                    (attribute.AttrNo == 17 && x.Attr17Id.HasValue) ||
                    (attribute.AttrNo == 18 && x.Attr18Id.HasValue) ||
                    (attribute.AttrNo == 19 && x.Attr19Id.HasValue)
                    ))
                {
                    throw new UserFriendlyException(L["Error:General:UpdateContraint:550"]);
                }
            }
            else {
                if (attribute.AttrNo > 0 && _itemAttributeRepository.FirstOrDefaultAsync(x => x.AttrNo == attribute.AttrNo - 1).Result?.Active != true)
                {
                    throw new UserFriendlyException(L["Error:General:UpdateContraint:550"]);
                }
            }
            
            //itemAttribute.AttrNo = input.AttrNo;
            attribute.AttrName = input.AttrName;
            attribute.Active = input.Active;
            attribute.HierarchyLevel = input.HierarchyLevel;

            attribute.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            var updatedRecord = await _itemAttributeRepository.UpdateAsync(attribute);

            return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(updatedRecord);
        }
    }
}
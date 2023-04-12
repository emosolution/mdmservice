using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.ItemGroups;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{

    [Authorize(MdmServicePermissions.ItemGroups.Default)]
    public partial class ItemGroupAttributesAppService
    {
        public virtual async Task<ItemGroupAttributeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroupAttribute, ItemGroupAttributeDto>(await _itemGroupAttributeRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var itemGroupAttribute = await _itemGroupAttributeRepository.GetAsync(id);
            await CheckItemGroup(itemGroupAttribute.ItemGroupId);
            await _itemGroupAttributeRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Create)]
        public virtual async Task<ItemGroupAttributeDto> CreateAsync(ItemGroupAttributeCreateDto input)
        {
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }
            await CheckItemGroup(input.ItemGroupId);
            await CheckAllAttributeValues(
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id,
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id,
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id,
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id);

            var itemGroupAttribute = await _itemGroupAttributeManager.CreateAsync(
                input.ItemGroupId,
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id,
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id,
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id,
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id,
                input.Description);

            return ObjectMapper.Map<ItemGroupAttribute, ItemGroupAttributeDto>(itemGroupAttribute);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Edit)]
        public virtual async Task<ItemGroupAttributeDto> UpdateAsync(Guid id, ItemGroupAttributeUpdateDto input)
        {
            var itemGroupAttribute = await _itemGroupAttributeRepository.GetAsync(id);
            await CheckItemGroup(itemGroupAttribute.ItemGroupId);
            await CheckAllAttributeValues(
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id,
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id,
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id,
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id);

            await _itemGroupAttributeManager.UpdateAsync(
                id,
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id,
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id,
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id,
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, 
                input.Description, input.ConcurrencyStamp);

            return ObjectMapper.Map<ItemGroupAttribute, ItemGroupAttributeDto>(itemGroupAttribute);
        }

        private async Task CheckItemGroup(Guid itemGroupId)
        {
            var itemGroup = await _itemGroupRepository.GetAsync(itemGroupId);
            if (itemGroup.Status != GroupStatus.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:ItemGroupAttributesAppService:550"], code: "1");
            }
            if (itemGroup.Type != GroupType.ATTRIBUTE)
            {
                throw new UserFriendlyException(message: L["Error:ItemGroupAttributesAppService:551"], code: "1");
            }
        }

        private async Task CheckAllAttributeValues(
            Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id,
            Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id,
            Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id,
            Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id)
        {
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
                await CheckAttributeValue((Guid) input.Item1, input.Item2);
            }
        }

        private async Task CheckAttributeValue(Guid attributeValueId, int attrNo)
        {
            var itemAttribute = await _itemAttributeRepository.GetAsync(x => x.AttrNo == attrNo - 1);
            if (!itemAttribute.Active)
            {
                throw new UserFriendlyException(
                    message: L["Error:ItemGroupAttributesAppService:552", attrNo.ToString()], code: "1");
            }
            var itemAttributeValue =
                await _itemAttributeValueRepository.GetAsync(x => x.Id == attributeValueId);
            if (itemAttributeValue.ItemAttributeId != itemAttribute.Id)
            {
                throw new UserFriendlyException(
                    message: L["Error:ItemGroupAttributesAppService:553", attrNo.ToString()], code: "1");
            }
        }
    }
}
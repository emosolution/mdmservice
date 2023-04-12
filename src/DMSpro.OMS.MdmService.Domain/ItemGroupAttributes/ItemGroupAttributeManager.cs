using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public class ItemGroupAttributeManager : DomainService
    {
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;

        public ItemGroupAttributeManager(IItemGroupAttributeRepository itemGroupAttributeRepository)
        {
            _itemGroupAttributeRepository = itemGroupAttributeRepository;
        }

        public async Task<ItemGroupAttribute> CreateAsync(
            Guid itemGroupId, 
            Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id,
            Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, 
            Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, 
            Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, 
            string description)
        {
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.Length(description, nameof(description), ItemGroupAttributeConsts.DescriptionMaxLength);

            var itemGroupAttribute = new ItemGroupAttribute(
             GuidGenerator.Create(),
             itemGroupId, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id, attr5Id, description
             );

            return await _itemGroupAttributeRepository.InsertAsync(itemGroupAttribute);
        }

        public async Task<ItemGroupAttribute> UpdateAsync(
            Guid id,
            Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, 
            Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, 
            Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, 
            Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, 
            string description, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.Length(description, nameof(description), ItemGroupAttributeConsts.DescriptionMaxLength);

            var itemGroupAttribute = await _itemGroupAttributeRepository.GetAsync(id);

            itemGroupAttribute.Attr0Id = attr0Id;
            itemGroupAttribute.Attr1Id = attr1Id;
            itemGroupAttribute.Attr2Id = attr2Id;
            itemGroupAttribute.Attr3Id = attr3Id;
            itemGroupAttribute.Attr4Id = attr4Id;
            itemGroupAttribute.Attr5Id = attr5Id;
            itemGroupAttribute.Attr6Id = attr6Id;
            itemGroupAttribute.Attr7Id = attr7Id;
            itemGroupAttribute.Attr8Id = attr8Id;
            itemGroupAttribute.Attr9Id = attr9Id;
            itemGroupAttribute.Attr10Id = attr10Id;
            itemGroupAttribute.Attr11Id = attr11Id;
            itemGroupAttribute.Attr12Id = attr12Id;
            itemGroupAttribute.Attr13Id = attr13Id;
            itemGroupAttribute.Attr14Id = attr14Id;
            itemGroupAttribute.Attr15Id = attr15Id;
            itemGroupAttribute.Attr16Id = attr16Id;
            itemGroupAttribute.Attr17Id = attr17Id;
            itemGroupAttribute.Attr18Id = attr18Id;
            itemGroupAttribute.Attr19Id = attr19Id;
            itemGroupAttribute.Description = description;

            itemGroupAttribute.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemGroupAttributeRepository.UpdateAsync(itemGroupAttribute);
        }

    }
}
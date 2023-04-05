using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValueManager : DomainService
    {
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;

        public ItemAttributeValueManager(IItemAttributeValueRepository itemAttributeValueRepository)
        {
            _itemAttributeValueRepository = itemAttributeValueRepository;
        }

        public async Task<ItemAttributeValue> CreateAsync(
        Guid itemAttributeId, Guid? parentId, string attrValName, string code)
        {
            Check.NotNull(itemAttributeId, nameof(itemAttributeId));
            Check.NotNullOrWhiteSpace(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), ItemAttributeValueConsts.AttrValNameMaxLength, ItemAttributeValueConsts.AttrValNameMinLength);

            var itemAttributeValue = new ItemAttributeValue(
             GuidGenerator.Create(),
             itemAttributeId, parentId, attrValName, code);

            return await _itemAttributeValueRepository.InsertAsync(itemAttributeValue);
        }

        public async Task<ItemAttributeValue> UpdateAsync(
            Guid id,
            Guid itemAttributeId, Guid? parentId, string attrValName, string code, 
            [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemAttributeId, nameof(itemAttributeId));
            Check.NotNullOrWhiteSpace(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), ItemAttributeValueConsts.AttrValNameMaxLength, ItemAttributeValueConsts.AttrValNameMinLength);
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), ItemAttributeValueConsts.CodeMaxLength, ItemAttributeValueConsts.CodeMinLength);

            var itemAttributeValue = await _itemAttributeValueRepository.GetAsync(id);

            itemAttributeValue.ItemAttributeId = itemAttributeId;
            itemAttributeValue.ParentId = parentId;
            itemAttributeValue.AttrValName = attrValName;
            itemAttributeValue.Code = code;

            itemAttributeValue.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemAttributeValueRepository.UpdateAsync(itemAttributeValue);
        }

    }
}
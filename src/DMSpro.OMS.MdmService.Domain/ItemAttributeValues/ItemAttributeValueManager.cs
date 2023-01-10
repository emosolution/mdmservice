using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
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
        Guid itemAttributeId, Guid? parentId, string attrValName)
        {
            Check.NotNull(itemAttributeId, nameof(itemAttributeId));
            Check.NotNullOrWhiteSpace(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), ItemAttributeValueConsts.AttrValNameMaxLength, ItemAttributeValueConsts.AttrValNameMinLength);

            var itemAttributeValue = new ItemAttributeValue(
             GuidGenerator.Create(),
             itemAttributeId, parentId, attrValName
             );

            return await _itemAttributeValueRepository.InsertAsync(itemAttributeValue);
        }

        public async Task<ItemAttributeValue> UpdateAsync(
            Guid id,
            Guid itemAttributeId, Guid? parentId, string attrValName, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemAttributeId, nameof(itemAttributeId));
            Check.NotNullOrWhiteSpace(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), ItemAttributeValueConsts.AttrValNameMaxLength, ItemAttributeValueConsts.AttrValNameMinLength);

            var queryable = await _itemAttributeValueRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var itemAttributeValue = await AsyncExecuter.FirstOrDefaultAsync(query);

            itemAttributeValue.ItemAttributeId = itemAttributeId;
            itemAttributeValue.ParentId = parentId;
            itemAttributeValue.AttrValName = attrValName;

            itemAttributeValue.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemAttributeValueRepository.UpdateAsync(itemAttributeValue);
        }

    }
}
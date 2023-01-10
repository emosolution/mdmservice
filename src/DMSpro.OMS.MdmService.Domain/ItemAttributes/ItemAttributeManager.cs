using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributeManager : DomainService
    {
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public ItemAttributeManager(IItemAttributeRepository itemAttributeRepository)
        {
            _itemAttributeRepository = itemAttributeRepository;
        }

        public async Task<ItemAttribute> CreateAsync(
        int attrNo, string attrName, bool active, bool isSellingCategory, int? hierarchyLevel = null)
        {
            Check.Range(attrNo, nameof(attrNo), ItemAttributeConsts.AttrNoMinLength, ItemAttributeConsts.AttrNoMaxLength);
            Check.NotNullOrWhiteSpace(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName), ItemAttributeConsts.AttrNameMaxLength, ItemAttributeConsts.AttrNameMinLength);

            var itemAttribute = new ItemAttribute(
             GuidGenerator.Create(),
             attrNo, attrName, active, isSellingCategory, hierarchyLevel
             );

            return await _itemAttributeRepository.InsertAsync(itemAttribute);
        }

        public async Task<ItemAttribute> UpdateAsync(
            Guid id,
            int attrNo, string attrName, bool active, bool isSellingCategory, int? hierarchyLevel = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.Range(attrNo, nameof(attrNo), ItemAttributeConsts.AttrNoMinLength, ItemAttributeConsts.AttrNoMaxLength);
            Check.NotNullOrWhiteSpace(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName), ItemAttributeConsts.AttrNameMaxLength, ItemAttributeConsts.AttrNameMinLength);

            var queryable = await _itemAttributeRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var itemAttribute = await AsyncExecuter.FirstOrDefaultAsync(query);

            itemAttribute.AttrNo = attrNo;
            itemAttribute.AttrName = attrName;
            itemAttribute.Active = active;
            itemAttribute.IsSellingCategory = isSellingCategory;
            itemAttribute.HierarchyLevel = hierarchyLevel;

            itemAttribute.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemAttributeRepository.UpdateAsync(itemAttribute);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public class ProductAttributeManager : DomainService
    {
        private readonly IProductAttributeRepository _productAttributeRepository;

        public ProductAttributeManager(IProductAttributeRepository productAttributeRepository)
        {
            _productAttributeRepository = productAttributeRepository;
        }

        public async Task<ProductAttribute> CreateAsync(
        int attrNo, string attrName, bool active, bool isProductCategory, int? hierarchyLevel = null)
        {
            Check.Range(attrNo, nameof(attrNo), ProductAttributeConsts.AttrNoMinLength, ProductAttributeConsts.AttrNoMaxLength);
            Check.NotNullOrWhiteSpace(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName), ProductAttributeConsts.AttrNameMaxLength, ProductAttributeConsts.AttrNameMinLength);

            var productAttribute = new ProductAttribute(
             GuidGenerator.Create(),
             attrNo, attrName, active, isProductCategory, hierarchyLevel
             );

            return await _productAttributeRepository.InsertAsync(productAttribute);
        }

        public async Task<ProductAttribute> UpdateAsync(
            Guid id,
            int attrNo, string attrName, bool active, bool isProductCategory, int? hierarchyLevel = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.Range(attrNo, nameof(attrNo), ProductAttributeConsts.AttrNoMinLength, ProductAttributeConsts.AttrNoMaxLength);
            Check.NotNullOrWhiteSpace(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName), ProductAttributeConsts.AttrNameMaxLength, ProductAttributeConsts.AttrNameMinLength);

            var queryable = await _productAttributeRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var productAttribute = await AsyncExecuter.FirstOrDefaultAsync(query);

            productAttribute.AttrNo = attrNo;
            productAttribute.AttrName = attrName;
            productAttribute.Active = active;
            productAttribute.IsProductCategory = isProductCategory;
            productAttribute.HierarchyLevel = hierarchyLevel;

            productAttribute.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _productAttributeRepository.UpdateAsync(productAttribute);
        }

    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public class ProductAttribute : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int AttrNo { get; set; }

        [NotNull]
        public virtual string AttrName { get; set; }

        public virtual int? HierarchyLevel { get; set; }

        public virtual bool Active { get; set; }

        public virtual bool IsProductCategory { get; set; }

        public ProductAttribute()
        {

        }

        public ProductAttribute(Guid id, int attrNo, string attrName, bool active, bool isProductCategory, int? hierarchyLevel = null)
        {

            Id = id;
            if (attrNo < ProductAttributeConsts.AttrNoMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(attrNo), attrNo, "The value of 'attrNo' cannot be lower than " + ProductAttributeConsts.AttrNoMinLength);
            }

            if (attrNo > ProductAttributeConsts.AttrNoMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(attrNo), attrNo, "The value of 'attrNo' cannot be greater than " + ProductAttributeConsts.AttrNoMaxLength);
            }

            Check.NotNull(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName), ProductAttributeConsts.AttrNameMaxLength, ProductAttributeConsts.AttrNameMinLength);
            if (hierarchyLevel < ProductAttributeConsts.HierarchyLevelMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(hierarchyLevel), hierarchyLevel, "The value of 'hierarchyLevel' cannot be lower than " + ProductAttributeConsts.HierarchyLevelMinLength);
            }

            if (hierarchyLevel > ProductAttributeConsts.HierarchyLevelMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(hierarchyLevel), hierarchyLevel, "The value of 'hierarchyLevel' cannot be greater than " + ProductAttributeConsts.HierarchyLevelMaxLength);
            }

            AttrNo = attrNo;
            AttrName = attrName;
            Active = active;
            IsProductCategory = isProductCategory;
            HierarchyLevel = hierarchyLevel;
        }

    }
}
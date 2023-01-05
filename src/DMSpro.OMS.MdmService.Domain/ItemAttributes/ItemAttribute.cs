using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttribute : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int AttrNo { get; set; }

        [NotNull]
        public virtual string AttrName { get; set; }

        public virtual int? HierarchyLevel { get; set; }

        public virtual bool Active { get; set; }

        public virtual bool IsSellingCategory { get; set; }

        public ItemAttribute()
        {

        }

        public ItemAttribute(Guid id, int attrNo, string attrName, bool active, bool isSellingCategory, int? hierarchyLevel = null)
        {

            Id = id;
            if (attrNo < ItemAttributeConsts.AttrNoMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(attrNo), attrNo, "The value of 'attrNo' cannot be lower than " + ItemAttributeConsts.AttrNoMinLength);
            }

            if (attrNo > ItemAttributeConsts.AttrNoMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(attrNo), attrNo, "The value of 'attrNo' cannot be greater than " + ItemAttributeConsts.AttrNoMaxLength);
            }

            Check.NotNull(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName), ItemAttributeConsts.AttrNameMaxLength, ItemAttributeConsts.AttrNameMinLength);
            AttrNo = attrNo;
            AttrName = attrName;
            Active = active;
            IsSellingCategory = isSellingCategory;
            HierarchyLevel = hierarchyLevel;
        }

    }
}
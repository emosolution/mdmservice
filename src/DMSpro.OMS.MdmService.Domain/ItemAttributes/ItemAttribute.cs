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

        [NotNull]
        public virtual string AttrNo { get; set; }

        [NotNull]
        public virtual string AttrName { get; set; }

        public virtual int? HierarchyLevel { get; set; }

        public virtual bool Active { get; set; }

        public virtual bool IsSellingCategory { get; set; }

        public ItemAttribute()
        {

        }

        public ItemAttribute(Guid id, string attrNo, string attrName, bool active, bool isSellingCategory, int? hierarchyLevel = null)
        {

            Id = id;
            Check.NotNull(attrNo, nameof(attrNo));
            Check.Length(attrNo, nameof(attrNo), ItemAttributeConsts.AttrNoMaxLength, ItemAttributeConsts.AttrNoMinLength);
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
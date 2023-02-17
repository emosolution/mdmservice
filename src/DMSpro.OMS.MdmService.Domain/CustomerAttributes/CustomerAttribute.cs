using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public partial class CustomerAttribute : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int AttrNo { get; set; }

        [NotNull]
        public virtual string AttrName { get; set; }

        public virtual int? HierarchyLevel { get; set; }

        public virtual bool Active { get; set; }

        public CustomerAttribute()
        {

        }

        public CustomerAttribute(Guid id, int attrNo, string attrName, bool active, int? hierarchyLevel = null)
        {

            Id = id;
            if (attrNo < CustomerAttributeConsts.AttrNoMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(attrNo), attrNo, "The value of 'attrNo' cannot be lower than " + CustomerAttributeConsts.AttrNoMinLength);
            }

            if (attrNo > CustomerAttributeConsts.AttrNoMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(attrNo), attrNo, "The value of 'attrNo' cannot be greater than " + CustomerAttributeConsts.AttrNoMaxLength);
            }

            Check.NotNull(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName), CustomerAttributeConsts.AttrNameMaxLength, CustomerAttributeConsts.AttrNameMinLength);
            if (hierarchyLevel < CustomerAttributeConsts.HierarchyLevelMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(hierarchyLevel), hierarchyLevel, "The value of 'hierarchyLevel' cannot be lower than " + CustomerAttributeConsts.HierarchyLevelMinLength);
            }

            if (hierarchyLevel > CustomerAttributeConsts.HierarchyLevelMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(hierarchyLevel), hierarchyLevel, "The value of 'hierarchyLevel' cannot be greater than " + CustomerAttributeConsts.HierarchyLevelMaxLength);
            }

            AttrNo = attrNo;
            AttrName = attrName;
            Active = active;
            HierarchyLevel = hierarchyLevel;
        }

    }
}
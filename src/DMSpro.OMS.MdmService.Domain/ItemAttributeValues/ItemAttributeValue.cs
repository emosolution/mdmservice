using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.ItemAttributeValues;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public partial class ItemAttributeValue : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string AttrValName { get; set; }
        public Guid ItemAttributeId { get; set; }
        public Guid? ParentId { get; set; }
        public virtual ItemAttribute ItemAttribute { get; set; }
        public virtual ItemAttributeValue Parent { get; set; }
        public ItemAttributeValue()
        {

        }

        public ItemAttributeValue(Guid id, Guid itemAttributeId, Guid? parentId, string attrValName)
        {

            Id = id;
            Check.NotNull(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), ItemAttributeValueConsts.AttrValNameMaxLength, ItemAttributeValueConsts.AttrValNameMinLength);
            AttrValName = attrValName;
            ItemAttributeId = itemAttributeId;
            ParentId = parentId;
        }

    }
}
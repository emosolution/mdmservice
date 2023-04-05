using System;
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

        [NotNull]
        public virtual string Code { get; set; }
        public Guid ItemAttributeId { get; set; }
        public Guid? ParentId { get; set; }

        public ItemAttributeValue()
        {

        }

        public ItemAttributeValue(Guid id, Guid itemAttributeId, Guid? parentId, string attrValName, string code)
        {

            Id = id;
            Check.NotNull(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), ItemAttributeValueConsts.AttrValNameMaxLength, ItemAttributeValueConsts.AttrValNameMinLength);
            AttrValName = attrValName;
            Code = code;
            ItemAttributeId = itemAttributeId;
            ParentId = parentId;
        }

    }
}
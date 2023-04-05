using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public partial class CustomerAttributeValue : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string AttrValName { get; set; }
        public Guid CustomerAttributeId { get; set; }
        public Guid? ParentId { get; set; }

        public CustomerAttributeValue()
        {

        }

        public CustomerAttributeValue(Guid id, Guid customerAttributeId, Guid? parentId, string code, string attrValName)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), CustomerAttributeValueConsts.CodeMaxLength, CustomerAttributeValueConsts.CodeMinLength);
            Check.NotNull(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), CustomerAttributeValueConsts.AttrValNameMaxLength, CustomerAttributeValueConsts.AttrValNameMinLength);
            Code = code;
            AttrValName = attrValName;
            CustomerAttributeId = customerAttributeId;
            ParentId = parentId;
        }

    }
}
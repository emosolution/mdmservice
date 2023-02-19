using DMSpro.OMS.MdmService.CustomerAttributes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public partial class CusAttributeValue : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string AttrValName { get; set; }
        public Guid CustomerAttributeId { get; set; }
        public Guid? ParentCusAttributeValueId { get; set; }
        public virtual CusAttributeValue Parent { get; set; }
        public virtual CustomerAttribute CustomerAttribute { get; set; }
        public CusAttributeValue()
        {

        }

        public CusAttributeValue(Guid id, Guid customerAttributeId, Guid? parentCusAttributeValueId, string attrValName)
        {

            Id = id;
            Check.NotNull(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), CusAttributeValueConsts.AttrValNameMaxLength, CusAttributeValueConsts.AttrValNameMinLength);
            AttrValName = attrValName;
            CustomerAttributeId = customerAttributeId;
            ParentCusAttributeValueId = parentCusAttributeValueId;
        }

    }
}
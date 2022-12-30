using DMSpro.OMS.MdmService.ProductAttributes;
using DMSpro.OMS.MdmService.ProdAttributeValues;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValue : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string AttrValName { get; set; }
        public Guid ProdAttributeId { get; set; }
        public Guid? ParentProdAttributeValueId { get; set; }

        public ProdAttributeValue()
        {

        }

        public ProdAttributeValue(Guid id, Guid prodAttributeId, Guid? parentProdAttributeValueId, string attrValName)
        {

            Id = id;
            Check.NotNull(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), ProdAttributeValueConsts.AttrValNameMaxLength, ProdAttributeValueConsts.AttrValNameMinLength);
            AttrValName = attrValName;
            ProdAttributeId = prodAttributeId;
            ParentProdAttributeValueId = parentProdAttributeValueId;
        }

    }
}
using System;
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

        public virtual bool Active { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        public CustomerAttribute()
        {

        }

        public CustomerAttribute(Guid id, int attrNo, string attrName, bool active)
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
            AttrNo = attrNo;
            AttrName = attrName;
            Active = active;
            Code = $"{attrNo}";
        }

    }
}
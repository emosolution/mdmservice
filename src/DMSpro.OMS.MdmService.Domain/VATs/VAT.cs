using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.VATs
{
    public partial class VAT : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public virtual uint Rate { get; set; }

        public VAT()
        {

        }

        public VAT(Guid id, string code, string name, uint rate)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), VATConsts.CodeMaxLength, VATConsts.CodeMinLength);
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), VATConsts.NameMaxLength, VATConsts.NameMinLength);
            if (rate < VATConsts.RateMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(rate), rate, "The value of 'rate' cannot be lower than " + VATConsts.RateMinLength);
            }

            if (rate > VATConsts.RateMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(rate), rate, "The value of 'rate' cannot be greater than " + VATConsts.RateMaxLength);
            }

            Code = code;
            Name = name;
            Rate = rate;
        }

    }
}
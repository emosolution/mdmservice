using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.Currencies
{
    public class Currency : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string Name { get; set; }

        public Currency()
        {

        }

        public Currency(Guid id, string code, string name)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), CurrencyConsts.CodeMaxLength, CurrencyConsts.CodeMinLength);
            Check.Length(name, nameof(name), CurrencyConsts.NameMaxLength, CurrencyConsts.NameMinLength);
            Code = code;
            Name = name;
        }

    }
}
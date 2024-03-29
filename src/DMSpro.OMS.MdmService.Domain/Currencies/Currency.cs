using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.Currencies
{
    public partial class Currency : FullAuditedAggregateRoot<Guid>, IMultiTenant
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
            Check.Length(name, nameof(name), CurrencyConsts.NameMaxLength, 0);
            Code = code;
            Name = name;
        }

    }
}
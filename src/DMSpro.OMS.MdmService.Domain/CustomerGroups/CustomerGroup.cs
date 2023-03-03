using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public partial class CustomerGroup : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string Name { get; set; }

        public virtual bool Active { get; set; }

        public virtual DateTime? EffectiveDate { get; set; }

        public virtual Type GroupBy { get; set; }

        public virtual Status Status { get; set; }

        public CustomerGroup()
        {

        }

        public CustomerGroup(Guid id, string code, string name, bool active, Type groupBy, Status status, DateTime? effectiveDate = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), CustomerGroupConsts.CodeMaxLength, CustomerGroupConsts.CodeMinLength);
            Check.Length(name, nameof(name), CustomerGroupConsts.NameMaxLength, 0);
            Code = code;
            Name = name;
            Active = active;
            GroupBy = groupBy;
            Status = status;
            EffectiveDate = effectiveDate;
        }

    }
}
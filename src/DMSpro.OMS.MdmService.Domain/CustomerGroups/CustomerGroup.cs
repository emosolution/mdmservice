using System;
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

        [NotNull]
        public virtual string Name { get; set; }

        public virtual bool Selectable { get; set; }

        public virtual Type GroupBy { get; set; }

        public virtual Status Status { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public CustomerGroup()
        {

        }

        public CustomerGroup(Guid id, string code, string name, bool selectable, Type groupBy, Status status, string description)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), CustomerGroupConsts.CodeMaxLength, CustomerGroupConsts.CodeMinLength);
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), CustomerGroupConsts.NameMaxLength, CustomerGroupConsts.NameMinLength);
            Check.Length(description, nameof(description), CustomerGroupConsts.DescriptionMaxLength, 0);
            Code = code;
            Name = name;
            Selectable = selectable;
            GroupBy = groupBy;
            Status = status;
            Description = description;
        }

    }
}
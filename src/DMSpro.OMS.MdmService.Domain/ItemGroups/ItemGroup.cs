using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public partial class ItemGroup : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual GroupType Type { get; set; }

        public virtual bool Selectable { get; set; }    

        public virtual GroupStatus Status { get; set; }

        public ItemGroup()
        {

        }

        public ItemGroup(Guid id, string code, string name, string description, GroupType type, GroupStatus status, bool selectable)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), ItemGroupConsts.CodeMaxLength, ItemGroupConsts.CodeMinLength);
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), ItemGroupConsts.NameMaxLength, 0);
            Check.Length(description, nameof(description), ItemGroupConsts.DescriptionMaxLength, 0);
            Code = code;
            Name = name;
            Description = description;
            Type = type;
            Status = status;
            Selectable = selectable;
        }
    }
}
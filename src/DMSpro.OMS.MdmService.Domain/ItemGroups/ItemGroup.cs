using DMSpro.OMS.MdmService.ItemGroups;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroup : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual GroupType Type { get; set; }

        public virtual GroupStatus Status { get; set; }

        public ItemGroup()
        {

        }

        public ItemGroup(Guid id, string code, string name, string description, GroupType type, GroupStatus status)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), ItemGroupConsts.CodeMaxLength, ItemGroupConsts.CodeMinLength);
            Check.NotNull(name, nameof(name));
            Check.Length(description, nameof(description), ItemGroupConsts.DescriptionMaxLength, ItemGroupConsts.DescriptionMinLength);
            Code = code;
            Name = name;
            Description = description;
            Type = type;
            Status = status;
        }

    }
}
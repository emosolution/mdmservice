using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public partial class WorkingPosition : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }

        public WorkingPosition()
        {

        }

        public WorkingPosition(Guid id, string code, string name, string description, bool active)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), WorkingPositionConsts.CodeMaxLength, WorkingPositionConsts.CodeMinLength);
            Check.Length(name, nameof(name), WorkingPositionConsts.NameMaxLength, 0);
            Check.Length(description, nameof(description), WorkingPositionConsts.DescriptionMaxLength, 0);
            Code = code;
            Name = name;
            Description = description;
            Active = active;
        }

    }
}
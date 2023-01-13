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
    public class WorkingPosition : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
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
            Check.NotNull(name, nameof(name));
            Code = code;
            Name = name;
            Description = description;
            Active = active;
        }

    }
}
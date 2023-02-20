using DMSpro.OMS.MdmService.EmployeeProfiles;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public partial class EmployeeAttachment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string url { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }
        public Guid EmployeeProfileId { get; set; }

        public virtual EmployeeProfile EmployeeProfile { get; set; }

        public EmployeeAttachment()
        {

        }

        public EmployeeAttachment(Guid id, Guid employeeProfileId, string url, string description, bool active)
        {

            Id = id;
            Check.NotNull(url, nameof(url));
            this.url = url;
            Description = description;
            Active = active;
            EmployeeProfileId = employeeProfileId;
        }

    }
}
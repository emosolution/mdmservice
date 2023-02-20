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

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public partial class EmployeeImage : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        [NotNull]
        public virtual string url { get; set; }

        public virtual bool Active { get; set; }

        public virtual bool IsAvatar { get; set; }
        public Guid EmployeeProfileId { get; set; }
        public virtual EmployeeProfile EmployeeProfile { get; set; }
        public EmployeeImage()
        {

        }

        public EmployeeImage(Guid id, Guid employeeProfileId, string description, string url, bool active, bool isAvatar)
        {

            Id = id;
            Check.NotNull(url, nameof(url));
            Description = description;
            this.url = url;
            Active = active;
            IsAvatar = isAvatar;
            EmployeeProfileId = employeeProfileId;
        }

    }
}
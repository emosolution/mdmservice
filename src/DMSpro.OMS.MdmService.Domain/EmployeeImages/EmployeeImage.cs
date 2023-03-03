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

        public virtual bool Active { get; set; }

        public virtual bool IsAvatar { get; set; }

        public virtual Guid FileId { get; set; }
        public Guid EmployeeProfileId { get; set; }
        public virtual EmployeeProfile EmployeeProfile { get; set; }
        public EmployeeImage()
        {

        }

        public EmployeeImage(Guid id, Guid employeeProfileId, string description, bool active, bool isAvatar, Guid fileId)
        {

            Id = id;
            Check.Length(description, nameof(description), EmployeeImageConsts.DescriptionMaxLength, 0);
            Description = description;
            Active = active;
            IsAvatar = isAvatar;
            FileId = fileId;
            EmployeeProfileId = employeeProfileId;
        }

    }
}
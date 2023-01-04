using DMSpro.OMS.MdmService.Companies;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual Guid IdentityUserId { get; set; }
        public Guid CompanyId { get; set; }

        public CompanyIdentityUserAssignment()
        {

        }

        public CompanyIdentityUserAssignment(Guid id, Guid companyId, Guid identityUserId)
        {

            Id = id;
            IdentityUserId = identityUserId;
            CompanyId = companyId;
        }

    }
}
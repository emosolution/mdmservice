using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using DMSpro.OMS.MdmService.Companies;
namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial class CompanyIdentityUserAssignment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual Guid IdentityUserId { get; set; }
        public virtual bool CurrentlySelected { get; set; }
        public virtual Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public CompanyIdentityUserAssignment()
        {
        }

        public CompanyIdentityUserAssignment(Guid id, Guid companyId, Guid identityUserId, bool currentlySelected = false)
        {
            Id = id;
            IdentityUserId = identityUserId;
            CurrentlySelected = currentlySelected;
            CompanyId = companyId;
        }

    }
}
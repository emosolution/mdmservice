using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public partial class EmployeeInZone : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual EmployeeProfile EmployeeProfile { get; set; }
        public virtual SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public EmployeeInZone()
        {

        }

        public EmployeeInZone(Guid id, Guid salesOrgHierarchyId, Guid employeeId, DateTime effectiveDate, DateTime? endDate = null)
        {

            Id = id;
            EffectiveDate = effectiveDate;
            EndDate = endDate;
            SalesOrgHierarchyId = salesOrgHierarchyId;
            EmployeeId = employeeId;
        }

    }
}
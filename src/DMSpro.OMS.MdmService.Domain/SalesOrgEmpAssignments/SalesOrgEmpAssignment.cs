using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public partial class SalesOrgEmpAssignment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual bool IsBase { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        [CanBeNull]
        public virtual string HierarchyCode { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeProfileId { get; set; }

        public SalesOrgEmpAssignment()
        {

        }

        public SalesOrgEmpAssignment(Guid id, Guid salesOrgHierarchyId, Guid employeeProfileId, bool isBase, DateTime effectiveDate, string hierarchyCode, DateTime? endDate = null)
        {
            Id = id;
            Check.Length(hierarchyCode, nameof(hierarchyCode), SalesOrgEmpAssignmentConsts.HierarchyCodeMaxLength, 0);
            IsBase = isBase;
            EffectiveDate = effectiveDate;
            HierarchyCode = hierarchyCode;
            EndDate = endDate;
            SalesOrgHierarchyId = salesOrgHierarchyId;
            EmployeeProfileId = employeeProfileId;
        }
    }
}
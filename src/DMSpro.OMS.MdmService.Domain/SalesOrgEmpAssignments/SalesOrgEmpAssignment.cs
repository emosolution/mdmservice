using DMSpro.OMS.MdmService.SalesOrgHierarchies;
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

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public partial class SalesOrgEmpAssignment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual bool IsBase { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeProfileId { get; set; }

        public virtual SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public virtual EmployeeProfile EmployeeProfile { get; set; }
        public SalesOrgEmpAssignment()
        {

        }

        public SalesOrgEmpAssignment(Guid id, Guid salesOrgHierarchyId, Guid employeeProfileId, bool isBase, DateTime effectiveDate, DateTime? endDate = null)
        {

            Id = id;
            IsBase = isBase;
            EffectiveDate = effectiveDate;
            EndDate = endDate;
            SalesOrgHierarchyId = salesOrgHierarchyId;
            EmployeeProfileId = employeeProfileId;
        }

    }
}
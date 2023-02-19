using DMSpro.OMS.MdmService.SalesOrgHierarchies;
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

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public partial class CompanyInZone : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual bool IsBase { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public CompanyInZone()
        {

        }

        public CompanyInZone(Guid id, Guid salesOrgHierarchyId, Guid companyId, DateTime effectiveDate, bool isBase, DateTime? endDate = null)
        {

            Id = id;
            EffectiveDate = effectiveDate;
            IsBase = isBase;
            EndDate = endDate;
            SalesOrgHierarchyId = salesOrgHierarchyId;
            CompanyId = companyId;
        }

    }
}
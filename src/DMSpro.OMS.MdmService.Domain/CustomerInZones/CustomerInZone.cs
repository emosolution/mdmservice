using System;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public partial class CustomerInZone : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual DateTime? EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual SalesOrgHierarchy SalesOrgHierarchy {get; set;}
        public CustomerInZone()
        {

        }

        public CustomerInZone(Guid id, Guid salesOrgHierarchyId, Guid customerId, DateTime? effectiveDate = null, DateTime? endDate = null)
        {

            Id = id;
            EffectiveDate = effectiveDate;
            EndDate = endDate;
            SalesOrgHierarchyId = salesOrgHierarchyId;
            CustomerId = customerId;
        }

    }
}
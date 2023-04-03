using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public partial class CustomerInZone : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid CustomerId { get; set; }

        public CustomerInZone()
        {

        }

        public CustomerInZone(Guid id, Guid salesOrgHierarchyId, Guid customerId, DateTime effectiveDate, DateTime? endDate = null)
        {

            Id = id;
            EffectiveDate = effectiveDate;
            EndDate = endDate;
            SalesOrgHierarchyId = salesOrgHierarchyId;
            CustomerId = customerId;
        }

    }
}
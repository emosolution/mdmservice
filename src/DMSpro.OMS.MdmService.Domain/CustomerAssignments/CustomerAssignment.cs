using System;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.Customers;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
    public partial class CustomerAssignment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Company Company { get; set; }
        public virtual Customer Customer { get; set; }


        public CustomerAssignment()
        {

        }

        public CustomerAssignment(Guid id, Guid companyId, Guid customerId, DateTime effectiveDate, DateTime? endDate = null)
        {

            Id = id;
            EffectiveDate = effectiveDate;
            EndDate = endDate;
            CompanyId = companyId;
            CustomerId = customerId;
        }

    }
}
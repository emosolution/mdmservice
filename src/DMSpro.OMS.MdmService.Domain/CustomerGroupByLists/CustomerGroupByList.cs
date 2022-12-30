using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class CustomerGroupByList : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual bool Active { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid CustomerId { get; set; }

        public CustomerGroupByList()
        {

        }

        public CustomerGroupByList(Guid id, Guid customerGroupId, Guid customerId, bool active)
        {

            Id = id;
            Active = active;
            CustomerGroupId = customerGroupId;
            CustomerId = customerId;
        }

    }
}
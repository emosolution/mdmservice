using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

using Volo.Abp;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public partial class CustomerGroupByList : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual bool Active { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid CustomerId { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }
        public virtual Customer Customer { get; set; }
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
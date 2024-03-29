using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public partial class CustomerGroupList : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CustomerGroupId { get; set; }

        public CustomerGroupList()
        {

        }

        public CustomerGroupList(Guid id, Guid customerId, Guid customerGroupId, string description, bool active)
        {

            Id = id;
            Check.Length(description, nameof(description), CustomerGroupListConsts.DescriptionMaxLength, 0);
            Description = description;
            Active = active;
            CustomerId = customerId;
            CustomerGroupId = customerGroupId;
        }

    }
}
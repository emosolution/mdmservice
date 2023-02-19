using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public partial class CustomerAttachment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string url { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public CustomerAttachment()
        {

        }

        public CustomerAttachment(Guid id, Guid customerId, string url, string description, bool active)
        {

            Id = id;
            Check.NotNull(url, nameof(url));
            this.url = url;
            Description = description;
            Active = active;
            CustomerId = customerId;
        }

    }
}
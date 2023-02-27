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

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }

        public virtual Guid FileId { get; set; }
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public CustomerAttachment()
        {

        }

        public CustomerAttachment(Guid id, Guid customerId, string description, bool active, Guid fileId)
        {

            Id = id;
            Check.Length(description, nameof(description), CustomerAttachmentConsts.DescriptionMaxLength, 0);
            Description = description;
            Active = active;
            FileId = fileId;
            CustomerId = customerId;
        }

    }
}
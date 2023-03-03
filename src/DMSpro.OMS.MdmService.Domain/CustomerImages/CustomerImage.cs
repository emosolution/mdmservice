using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public partial class CustomerImage : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }

        public virtual bool IsAvatar { get; set; }

        public virtual bool IsPOSM { get; set; }

        public virtual Guid FileId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? POSMItemId { get; set; }

        public CustomerImage()
        {

        }

        public CustomerImage(Guid id, Guid customerId, Guid? pOSMItemId, string description, bool active, bool isAvatar, bool isPOSM, Guid fileId)
        {

            Id = id;
            Check.Length(description, nameof(description), CustomerImageConsts.DescriptionMaxLength, 0);
            Description = description;
            Active = active;
            IsAvatar = isAvatar;
            IsPOSM = isPOSM;
            FileId = fileId;
            CustomerId = customerId;
            POSMItemId = pOSMItemId;
        }

    }
}
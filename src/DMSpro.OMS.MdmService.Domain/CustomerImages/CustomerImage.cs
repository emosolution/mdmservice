using DMSpro.OMS.MdmService.Customers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class CustomerImage : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }

        public virtual bool IsAvatar { get; set; }

        public virtual bool IsPOSM { get; set; }

        public virtual Guid FileId { get; set; }
        public Guid CustomerId { get; set; }

        public CustomerImage()
        {

        }

        public CustomerImage(Guid id, Guid customerId, string description, bool active, bool isAvatar, bool isPOSM, Guid fileId)
        {

            Id = id;
            Check.Length(description, nameof(description), CustomerImageConsts.DescriptionMaxLength, 0);
            Description = description;
            Active = active;
            IsAvatar = isAvatar;
            IsPOSM = isPOSM;
            FileId = fileId;
            CustomerId = customerId;
        }

    }
}
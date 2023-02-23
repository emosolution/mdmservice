using DMSpro.OMS.MdmService.Items;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public partial class ItemAttachment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }

        public virtual Guid FileId { get; set; }
        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }
        public ItemAttachment()
        {

        }

        public ItemAttachment(Guid id, Guid itemId, string description, bool active, Guid fileId)
        {

            Id = id;
            Check.Length(description, nameof(description), ItemAttachmentConsts.DescriptionMaxLength, 0);
            Description = description;
            Active = active;
            FileId = fileId;
            ItemId = itemId;
        }

    }
}
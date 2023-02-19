using System;
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

        [NotNull]
        public virtual string Url { get; set; }

        public virtual bool Active { get; set; }

        public virtual Guid FileId { get; set; }
        public Guid ItemId { get; set; }

        public ItemAttachment()
        {

        }

        public ItemAttachment(Guid id, Guid itemId, string description, string url, bool active, Guid fileId)
        {

            Id = id;
            Check.Length(description, nameof(description), ItemAttachmentConsts.DescriptionMaxLength, 0);
            Check.NotNull(url, nameof(url));
            Check.Length(url, nameof(url), ItemAttachmentConsts.UrlMaxLength, ItemAttachmentConsts.UrlMinLength);
            Description = description;
            Url = url;
            Active = active;
            FileId = fileId;
            ItemId = itemId;
        }

    }
}
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

namespace DMSpro.OMS.MdmService.ItemImages
{
    public partial class ItemImage : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        [NotNull]
        public virtual string Url { get; set; }

        public virtual bool Active { get; set; }

        public virtual int DisplayOrder { get; set; }
        public Guid ItemId { get; set; }

        public ItemImage()
        {

        }

        public ItemImage(Guid id, Guid itemId, string description, string url, bool active, int displayOrder)
        {

            Id = id;
            Check.Length(description, nameof(description), ItemImageConsts.DescriptionMaxLength, 0);
            Check.NotNull(url, nameof(url));
            Check.Length(url, nameof(url), ItemImageConsts.UrlMaxLength, ItemImageConsts.UrlMinLength);
            Description = description;
            Url = url;
            Active = active;
            DisplayOrder = displayOrder;
            ItemId = itemId;
        }

    }
}
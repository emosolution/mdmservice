using DMSpro.OMS.MdmService.ItemMasters;
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
    public class ItemImage : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }

        [NotNull]
        public virtual string URL { get; set; }

        public virtual int DisplayOrder { get; set; }
        public Guid ItemId { get; set; }

        public ItemImage()
        {

        }

        public ItemImage(Guid id, Guid itemId, string description, bool active, string uRL, int displayOrder)
        {

            Id = id;
            Check.NotNull(uRL, nameof(uRL));
            Description = description;
            Active = active;
            URL = uRL;
            DisplayOrder = displayOrder;
            ItemId = itemId;
        }

    }
}
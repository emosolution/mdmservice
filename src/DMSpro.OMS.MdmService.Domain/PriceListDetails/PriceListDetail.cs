using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.UOMs;
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

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public partial class PriceListDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual decimal Price { get; set; }

        public virtual decimal? BasedOnPrice { get; set; }

        [NotNull]
        public virtual string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid UOMId { get; set; }
        public Guid ItemId { get; set; }
        public virtual PriceList PriceList { get; set; }
        public virtual UOM UOM { get; set; }
        public virtual Item Item { get; set; }

        public PriceListDetail()
        {

        }

        public PriceListDetail(Guid id, Guid priceListId, Guid uOMId, Guid itemId, decimal price, string description, decimal? basedOnPrice = null)
        {

            Id = id;
            Check.NotNull(description, nameof(description));
            Price = price;
            Description = description;
            BasedOnPrice = basedOnPrice;
            PriceListId = priceListId;
            UOMId = uOMId;
            ItemId = itemId;
        }

    }
}
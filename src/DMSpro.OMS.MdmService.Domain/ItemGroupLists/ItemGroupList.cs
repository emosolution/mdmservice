using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.UOMs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public partial class ItemGroupList : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int Rate { get; set; }

        public virtual decimal Price { get; set; }
        public Guid ItemGroupId { get; set; }
        public Guid ItemId { get; set; }
        public Guid UomId { get; set; }

        public virtual ItemGroup ItemGroup { get; set; }
        public virtual Item Item { get; set; }
        public virtual UOM UOM { get; set; }
        public ItemGroupList()
        {

        }

        public ItemGroupList(Guid id, Guid itemGroupId, Guid itemId, Guid uomId, int rate, decimal price)
        {

            Id = id;
            Rate = rate;
            Price = price;
            ItemGroupId = itemGroupId;
            ItemId = itemId;
            UomId = uomId;
        }

    }
}
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.ItemMasters;
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
    public class ItemGroupList : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int Rate { get; set; }
        public Guid ItemGroupId { get; set; }
        public Guid ItemId { get; set; }
        public Guid UOMId { get; set; }

        public ItemGroupList()
        {

        }

        public ItemGroupList(Guid id, Guid itemGroupId, Guid itemId, Guid uomId, int rate)
        {

            Id = id;
            Rate = rate;
            ItemGroupId = itemGroupId;
            ItemId = itemId;
            UOMId = uomId;
        }

    }
}
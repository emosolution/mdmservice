using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public partial class ItemGroupList : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int? Rate { get; set; }

        public virtual decimal? Price { get; set; }
        public Guid ItemGroupId { get; set; }
        public Guid ItemId { get; set; }
        public Guid? UomId { get; set; }

        public ItemGroupList()
        {

        }

        public ItemGroupList(Guid id, Guid itemGroupId, Guid itemId, Guid? uomId, int? rate = null, decimal? price = null)
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
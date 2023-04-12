using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public partial class PriceUpdateDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }
        public virtual decimal PriceBeforeUpdate { get; set; }
        public virtual decimal NewPrice { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public Guid PriceUpdateId { get; set; }
        public Guid PriceListDetailId { get; set; }
       
        public PriceUpdateDetail()
        {
        }

        public PriceUpdateDetail(Guid id, Guid priceUpdateId, Guid priceListDetailId, 
            decimal priceBeforeUpdate, decimal newPrice, DateTime? updatedDate = null)
        {

            Id = id;
            PriceBeforeUpdate = priceBeforeUpdate;
            NewPrice = newPrice;
            UpdatedDate = updatedDate;
            PriceUpdateId = priceUpdateId;
            PriceListDetailId = priceListDetailId;
        }
    }
}
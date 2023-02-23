using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.PriceListDetails;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public partial class PriceUpdateDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int PriceBeforeUpdate { get; set; }

        public virtual int NewPrice { get; set; }

        public virtual DateTime? UpdatedDate { get; set; }
        public Guid PriceUpdateId { get; set; }
        public Guid PriceListDetailId { get; set; }
        public virtual PriceUpdate PriceUpdate { get; set; }
        public virtual PriceListDetail PriceListDetail { get; set; }
        public PriceUpdateDetail()
        {

        }

        public PriceUpdateDetail(Guid id, Guid priceUpdateId, Guid priceListDetailId, int priceBeforeUpdate, int newPrice, DateTime? updatedDate = null)
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
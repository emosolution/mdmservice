using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public partial class PriceUpdate : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual PriceUpdateStatus Status { get; set; }

        public virtual DateTime? UpdateStatusDate { get; set; }
        public Guid PriceListId { get; set; }
        public virtual PriceList PriceList { get; set; }
        public PriceUpdate()
        {

        }

        public PriceUpdate(Guid id, Guid priceListId, string code, string description, DateTime effectiveDate, PriceUpdateStatus status, DateTime? updateStatusDate = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), PriceUpdateConsts.CodeMaxLength, PriceUpdateConsts.CodeMinLength);
            Check.Length(description, nameof(description), PriceUpdateConsts.DescriptionMaxLength, 0);
            Code = code;
            Description = description;
            EffectiveDate = effectiveDate;
            Status = status;
            UpdateStatusDate = updateStatusDate;
            PriceListId = priceListId;
        }

    }
}
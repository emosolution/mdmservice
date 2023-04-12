using System;
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

        public virtual DateTime? EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual PriceUpdateStatus Status { get; set; }

        public virtual bool IsScheduled { get; set; }

        public virtual DateTime? ReleasedDate { get; set; }

        public virtual DateTime? CancelledDate { get; set; }

        public virtual DateTime? CompleteDate { get; set; }
        public Guid PriceListId { get; set; }

        public PriceUpdate()
        {

        }

        public PriceUpdate(
            Guid id, 
            Guid priceListId, string code, string description, 
            PriceUpdateStatus status, bool isScheduled, 
            DateTime? effectiveDate = null, DateTime? endDate = null, 
            DateTime? releasedDate = null, DateTime? cancelledDate = null, 
            DateTime? completeDate = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), PriceUpdateConsts.CodeMaxLength, PriceUpdateConsts.CodeMinLength);
            Check.Length(description, nameof(description), PriceUpdateConsts.DescriptionMaxLength, 0);
            Code = code;
            Description = description;
            Status = status;
            IsScheduled = isScheduled;
            EffectiveDate = effectiveDate;
            EndDate = endDate;
            ReleasedDate = releasedDate;
            CancelledDate = cancelledDate;
            CompleteDate = completeDate;
            PriceListId = priceListId;
        }

    }
}
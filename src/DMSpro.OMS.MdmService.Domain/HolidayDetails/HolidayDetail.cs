using DMSpro.OMS.MdmService.Holidays;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public partial class HolidayDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }
        public Guid HolidayId { get; set; }
        public HolidayDetail()
        {
        }

        public HolidayDetail(Guid id, Guid holidayId, DateTime startDate, DateTime endDate, string description)
        {
            Id = id;
            Check.Length(description, nameof(description), HolidayDetailConsts.DescriptionMaxLength, 0);
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            HolidayId = holidayId;
        }
    }
}
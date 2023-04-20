using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp;
using JetBrains.Annotations;

namespace DMSpro.OMS.MdmService.Holidays
{
    public partial class Holiday : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        [NotNull]
        public virtual string Code { get; set; }
        
        public virtual Guid? TenantId { get; set; }

        public virtual int Year { get; set; }

        public virtual string Description { get; set; }

        public Holiday()
        {

        }

        public Holiday(Guid id, int year, string description)
        {

            Id = id;
            if (year < HolidayConsts.YearMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, "The value of 'year' cannot be lower than " + HolidayConsts.YearMinLength);
            }

            if (year > HolidayConsts.YearMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, "The value of 'year' cannot be greater than " + HolidayConsts.YearMaxLength);
            }

            Check.Length(description, nameof(description), HolidayConsts.DescriptionMaxLength);
            Year = year;
            Description = description;
            Code = year.ToString();
        }
    }
}
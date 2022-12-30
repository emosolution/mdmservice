using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class Holiday : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int Year { get; set; }

        [NotNull]
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

            Check.NotNull(description, nameof(description));
            Year = year;
            Description = description;
        }

    }
}
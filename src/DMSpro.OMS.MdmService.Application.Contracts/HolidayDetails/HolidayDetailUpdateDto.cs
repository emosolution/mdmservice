using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using JetBrains.Annotations;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailUpdateDto : IHasConcurrencyStamp
    {
        [NotNull]
        public DateTime StartDate { get; set; }
        [NotNull]
        public DateTime EndDate { get; set; }
        [StringLength(HolidayDetailConsts.DescriptionMaxLength)]
        public string Description { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailCreateDto
    {
        [NotNull]
        public DateTime StartDate { get; set; }
        [NotNull]
        public DateTime EndDate { get; set; }
        [StringLength(HolidayDetailConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        [NotNull]
        public Guid HolidayId { get; set; }
    }
}
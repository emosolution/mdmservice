using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailCreateDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [StringLength(HolidayDetailConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public Guid HolidayId { get; set; }
    }
}
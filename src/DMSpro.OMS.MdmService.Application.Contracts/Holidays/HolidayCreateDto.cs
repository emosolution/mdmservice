using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class HolidayCreateDto
    {
        [Required]
        [Range(HolidayConsts.YearMinLength, HolidayConsts.YearMaxLength)]
        public int Year { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
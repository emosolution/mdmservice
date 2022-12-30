using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public class WeightMeasurementCreateDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        [StringLength(WeightMeasurementConsts.NameMaxLength, MinimumLength = WeightMeasurementConsts.NameMinLength)]
        public string Name { get; set; }
        [Required]
        public uint Value { get; set; } = 1;
    }
}
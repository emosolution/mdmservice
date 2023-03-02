using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public class WeightMeasurementCreateDto
    {
        [Required]
        public string Code { get; set; }
        [StringLength(WeightMeasurementConsts.NameMaxLength)]
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public class DimensionMeasurementCreateDto
    {
        [Required]
        [StringLength(DimensionMeasurementConsts.CodeMaxLength, MinimumLength = DimensionMeasurementConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(DimensionMeasurementConsts.NameMaxLength, MinimumLength = DimensionMeasurementConsts.NameMinLength)]
        public string Name { get; set; }
        [Required]
        public uint Value { get; set; } = 1;
    }
}
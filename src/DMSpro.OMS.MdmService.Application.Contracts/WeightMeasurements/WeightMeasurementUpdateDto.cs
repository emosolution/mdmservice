using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public class WeightMeasurementUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public string Code { get; set; }
        [Required]
        [StringLength(WeightMeasurementConsts.NameMaxLength, MinimumLength = WeightMeasurementConsts.NameMinLength)]
        public string Name { get; set; }
        [Required]
        public uint Value { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
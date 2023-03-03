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
        [StringLength(WeightMeasurementConsts.NameMaxLength)]
        public string Name { get; set; }
        public decimal Value { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
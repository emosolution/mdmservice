using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public class DimensionMeasurementUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(DimensionMeasurementConsts.CodeMaxLength, MinimumLength = DimensionMeasurementConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(DimensionMeasurementConsts.NameMaxLength)]
        public string Name { get; set; }
        public decimal Value { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
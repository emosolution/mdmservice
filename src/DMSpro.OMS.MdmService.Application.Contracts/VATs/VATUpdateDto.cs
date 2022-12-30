using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.VATs
{
    public class VATUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(VATConsts.CodeMaxLength, MinimumLength = VATConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(VATConsts.NameMaxLength, MinimumLength = VATConsts.NameMinLength)]
        public string Name { get; set; }
        [Required]
        [Range(VATConsts.RateMinLength, VATConsts.RateMaxLength)]
        public uint Rate { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
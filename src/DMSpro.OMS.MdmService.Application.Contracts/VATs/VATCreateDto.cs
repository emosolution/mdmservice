using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.VATs
{
    public class VATCreateDto
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
    }
}
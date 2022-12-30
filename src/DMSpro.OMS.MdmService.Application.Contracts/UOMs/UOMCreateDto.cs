using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.UOMs
{
    public class UOMCreateDto
    {
        [Required]
        [StringLength(UOMConsts.CodeMaxLength, MinimumLength = UOMConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(UOMConsts.NameMaxLength, MinimumLength = UOMConsts.NameMinLength)]
        public string Name { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.UOMGroups
{
    public class UOMGroupCreateDto
    {
        [Required]
        [StringLength(UOMGroupConsts.CodeMaxLength, MinimumLength = UOMGroupConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(UOMGroupConsts.NameMaxLength, MinimumLength = UOMGroupConsts.NameMinLength)]
        public string Name { get; set; }
    }
}
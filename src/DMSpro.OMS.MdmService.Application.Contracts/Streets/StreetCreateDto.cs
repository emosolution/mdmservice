using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Streets
{
    public class StreetCreateDto
    {
        [Required]
        [RegularExpression(@"[a-zA-Z0-9][a-zA-Z0-9 ][^\t\n\r\f\v]+")]
        [StringLength(StreetConsts.NameMaxLength, MinimumLength = StreetConsts.NameMinLength)]
        public string Name { get; set; }
    }
}
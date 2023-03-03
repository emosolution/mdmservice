using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Currencies
{
    public class CurrencyCreateDto
    {
        [Required]
        [StringLength(CurrencyConsts.CodeMaxLength, MinimumLength = CurrencyConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(CurrencyConsts.NameMaxLength)]
        public string Name { get; set; }
    }
}
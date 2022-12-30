using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Currencies
{
    public class CurrencyUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CurrencyConsts.CodeMaxLength, MinimumLength = CurrencyConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(CurrencyConsts.NameMaxLength, MinimumLength = CurrencyConsts.NameMinLength)]
        public string Name { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
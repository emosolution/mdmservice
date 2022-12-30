using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.UOMs
{
    public class UOMUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(UOMConsts.CodeMaxLength, MinimumLength = UOMConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(UOMConsts.NameMaxLength, MinimumLength = UOMConsts.NameMinLength)]
        public string Name { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
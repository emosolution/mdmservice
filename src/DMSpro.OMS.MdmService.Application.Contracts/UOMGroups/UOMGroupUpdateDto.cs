using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.UOMGroups
{
    public class UOMGroupUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(UOMGroupConsts.CodeMaxLength, MinimumLength = UOMGroupConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(UOMGroupConsts.NameMaxLength, MinimumLength = UOMGroupConsts.NameMinLength)]
        public string Name { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SalesChannels
{
    public class SalesChannelUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(SalesChannelConsts.CodeMaxLength, MinimumLength = SalesChannelConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(SalesChannelConsts.NameMaxLength, MinimumLength = SalesChannelConsts.NameMinLength)]
        public string Name { get; set; }
        [StringLength(SalesChannelConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
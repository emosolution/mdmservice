using DMSpro.OMS.MdmService.ItemGroups;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(ItemGroupConsts.CodeMaxLength, MinimumLength = ItemGroupConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(ItemGroupConsts.NameMaxLength)]
        public string Name { get; set; }
        [StringLength(ItemGroupConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public GroupType Type { get; set; }
        public GroupStatus Status { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
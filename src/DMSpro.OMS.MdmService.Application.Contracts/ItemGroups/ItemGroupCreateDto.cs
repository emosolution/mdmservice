using DMSpro.OMS.MdmService.ItemGroups;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupCreateDto
    {
        [Required]
        [StringLength(ItemGroupConsts.CodeMaxLength, MinimumLength = ItemGroupConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(ItemGroupConsts.NameMaxLength)]
        public string Name { get; set; }
        [StringLength(ItemGroupConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public GroupType Type { get; set; } = ((GroupType[])Enum.GetValues(typeof(GroupType)))[0];
        public GroupStatus Status { get; set; } = ((GroupStatus[])Enum.GetValues(typeof(GroupStatus)))[0];
    }
}
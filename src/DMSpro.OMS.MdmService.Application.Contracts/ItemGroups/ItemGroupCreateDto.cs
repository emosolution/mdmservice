using System;
using System.ComponentModel.DataAnnotations;

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
        public bool? Selectable { get; set; }
    }
}
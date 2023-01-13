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
        public string Name { get; set; }
        [StringLength(ItemGroupConsts.DescriptionMaxLength, MinimumLength = ItemGroupConsts.DescriptionMinLength)]
        public string Description { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public GroupType Type { get; set; } = ((GroupType[])Enum.GetValues(typeof(GroupType)))[0];
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public GroupStatus Status { get; set; } = ((GroupStatus[])Enum.GetValues(typeof(GroupStatus)))[0];
    }
}
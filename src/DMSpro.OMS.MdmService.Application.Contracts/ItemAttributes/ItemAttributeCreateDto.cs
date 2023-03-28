using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributeCreateDto
    {
        [Range(ItemAttributeConsts.AttrNoMinLength, ItemAttributeConsts.AttrNoMaxLength)]
        public int AttrNo { get; set; }
        [Required]
        [StringLength(ItemAttributeConsts.AttrNameMaxLength, MinimumLength = ItemAttributeConsts.AttrNameMinLength)]
        public string AttrName { get; set; }
        public int? HierarchyLevel { get; set; }
        public bool Active { get; set; } = false;
    }
}
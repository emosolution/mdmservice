using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributeCreateDto
    {
        [Required]
        [StringLength(ItemAttributeConsts.AttrNoMaxLength, MinimumLength = ItemAttributeConsts.AttrNoMinLength)]
        public string AttrNo { get; set; }
        [Required]
        [StringLength(ItemAttributeConsts.AttrNameMaxLength, MinimumLength = ItemAttributeConsts.AttrNameMinLength)]
        public string AttrName { get; set; }
        public int? HierarchyLevel { get; set; }
        public bool Active { get; set; } = false;
        public bool IsSellingCategory { get; set; } = false;
    }
}
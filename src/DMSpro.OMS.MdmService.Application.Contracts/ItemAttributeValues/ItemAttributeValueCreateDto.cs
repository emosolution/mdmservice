using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValueCreateDto
    {
        [Required]
        [StringLength(ItemAttributeValueConsts.AttrValNameMaxLength, MinimumLength = ItemAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        public Guid ItemAttributeId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
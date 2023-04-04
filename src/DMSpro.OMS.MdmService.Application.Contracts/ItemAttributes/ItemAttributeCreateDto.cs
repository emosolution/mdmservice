using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributeCreateDto
    {
        [Required]
        [StringLength(ItemAttributeConsts.AttrNameMaxLength, MinimumLength = ItemAttributeConsts.AttrNameMinLength)]
        public string AttrName { get; set; }
    }
}
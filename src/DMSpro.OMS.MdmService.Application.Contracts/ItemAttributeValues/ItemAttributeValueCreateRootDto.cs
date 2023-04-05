using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValueCreateRootDto
    {
        [Required]
        [StringLength(ItemAttributeValueConsts.AttrValNameMaxLength, MinimumLength = ItemAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        [Required]
        [StringLength(ItemAttributeValueConsts.CodeMaxLength, MinimumLength = ItemAttributeValueConsts.CodeMinLength)]
        public string Code { get; set; }
        public Guid ItemAttributeId { get; set; }
    }
}
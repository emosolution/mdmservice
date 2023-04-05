using System.ComponentModel.DataAnnotations;
using System;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValueCreateHierarchyDto
    {
        [Required]
        [StringLength(ItemAttributeValueConsts.AttrValNameMaxLength, MinimumLength = ItemAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        [Required]
        [StringLength(ItemAttributeValueConsts.CodeMaxLength, MinimumLength = ItemAttributeValueConsts.CodeMinLength)]
        public string Code { get; set; }
        public Guid ParentId { get; set; }
    }
}
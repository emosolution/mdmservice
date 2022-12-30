using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public class ProductAttributeCreateDto
    {
        [Range(ProductAttributeConsts.AttrNoMinLength, ProductAttributeConsts.AttrNoMaxLength)]
        public int AttrNo { get; set; }
        [Required]
        [StringLength(ProductAttributeConsts.AttrNameMaxLength, MinimumLength = ProductAttributeConsts.AttrNameMinLength)]
        public string AttrName { get; set; }
        [Range(ProductAttributeConsts.HierarchyLevelMinLength, ProductAttributeConsts.HierarchyLevelMaxLength)]
        public int? HierarchyLevel { get; set; }
        public bool Active { get; set; } = false;
        public bool IsProductCategory { get; set; } = false;
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributeCreateDto
    {
        [Range(CustomerAttributeConsts.AttrNoMinLength, CustomerAttributeConsts.AttrNoMaxLength)]
        public int AttrNo { get; set; }
        [Required]
        [StringLength(CustomerAttributeConsts.AttrNameMaxLength, MinimumLength = CustomerAttributeConsts.AttrNameMinLength)]
        public string AttrName { get; set; }
        [Range(CustomerAttributeConsts.HierarchyLevelMinLength, CustomerAttributeConsts.HierarchyLevelMaxLength)]
        public int? HierarchyLevel { get; set; }
        public bool Active { get; set; } = true;
    }
}
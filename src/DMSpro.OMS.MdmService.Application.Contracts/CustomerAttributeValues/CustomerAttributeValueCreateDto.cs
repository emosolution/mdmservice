using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class CustomerAttributeValueCreateDto
    {
        [Required]
        [StringLength(CustomerAttributeValueConsts.CodeMaxLength, MinimumLength = CustomerAttributeValueConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(CustomerAttributeValueConsts.AttrValNameMaxLength, MinimumLength = CustomerAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        public Guid CustomerAttributeId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
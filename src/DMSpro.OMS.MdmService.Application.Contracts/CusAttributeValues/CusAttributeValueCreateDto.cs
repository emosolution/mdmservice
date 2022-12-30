using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class CusAttributeValueCreateDto
    {
        [Required]
        [StringLength(CusAttributeValueConsts.AttrValNameMaxLength, MinimumLength = CusAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        public Guid CustomerAttributeId { get; set; }
        public Guid? ParentCusAttributeValueId { get; set; }
    }
}
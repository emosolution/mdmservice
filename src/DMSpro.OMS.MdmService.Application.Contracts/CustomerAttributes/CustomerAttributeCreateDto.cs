using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributeCreateDto
    {
        [Required]
        [StringLength(CustomerAttributeConsts.AttrNameMaxLength, MinimumLength = CustomerAttributeConsts.AttrNameMinLength)]
        public string AttrName { get; set; }
    }
}
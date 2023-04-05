using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class CustomerAttributeValueUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CustomerAttributeValueConsts.CodeMaxLength, MinimumLength = CustomerAttributeValueConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(CustomerAttributeValueConsts.AttrValNameMaxLength, MinimumLength = CustomerAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        public Guid CustomerAttributeId { get; set; }
        public Guid? ParentId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
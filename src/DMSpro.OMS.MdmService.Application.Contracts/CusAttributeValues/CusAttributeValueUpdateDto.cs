using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class CusAttributeValueUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CusAttributeValueConsts.AttrValNameMaxLength, MinimumLength = CusAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        public Guid CustomerAttributeId { get; set; }
        public Guid? ParentCusAttributeValueId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
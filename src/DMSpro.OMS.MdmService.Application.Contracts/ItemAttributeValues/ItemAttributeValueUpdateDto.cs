using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValueUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(ItemAttributeValueConsts.AttrValNameMaxLength, MinimumLength = ItemAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        public Guid ItemAttributeId { get; set; }
        public Guid? ParentId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
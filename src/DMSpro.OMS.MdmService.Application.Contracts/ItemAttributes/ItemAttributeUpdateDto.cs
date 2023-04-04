using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributeUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(ItemAttributeConsts.AttrNameMaxLength, MinimumLength = ItemAttributeConsts.AttrNameMinLength)]
        public string AttrName { get; set; }
        
        public string ConcurrencyStamp { get; set; }
    }
}
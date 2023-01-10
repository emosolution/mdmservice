using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributeUpdateDto : IHasConcurrencyStamp
    {
        [Range(ItemAttributeConsts.AttrNoMinLength, ItemAttributeConsts.AttrNoMaxLength)]
        public int AttrNo { get; set; }
        [Required]
        [StringLength(ItemAttributeConsts.AttrNameMaxLength, MinimumLength = ItemAttributeConsts.AttrNameMinLength)]
        public string AttrName { get; set; }
        public int? HierarchyLevel { get; set; }
        public bool Active { get; set; }
        public bool IsSellingCategory { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
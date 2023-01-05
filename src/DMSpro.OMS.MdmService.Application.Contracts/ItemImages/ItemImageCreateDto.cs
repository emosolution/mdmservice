using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageCreateDto
    {
        [StringLength(ItemImageConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        [StringLength(ItemImageConsts.UrlMaxLength, MinimumLength = ItemImageConsts.UrlMinLength)]
        public string Url { get; set; }
        public bool Active { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;
        public Guid ItemId { get; set; }
    }
}
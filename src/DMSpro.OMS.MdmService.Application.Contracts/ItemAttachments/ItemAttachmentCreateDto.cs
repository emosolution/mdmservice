using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentCreateDto
    {
        [StringLength(ItemAttachmentConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        [StringLength(ItemAttachmentConsts.UrlMaxLength, MinimumLength = ItemAttachmentConsts.UrlMinLength)]
        public string Url { get; set; }
        public bool Active { get; set; } = true;
        public Guid FileId { get; set; }
        public Guid ItemId { get; set; }
    }
}
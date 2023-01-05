using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(ItemAttachmentConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        [StringLength(ItemAttachmentConsts.UrlMaxLength, MinimumLength = ItemAttachmentConsts.UrlMinLength)]
        public string Url { get; set; }
        public bool Active { get; set; }
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
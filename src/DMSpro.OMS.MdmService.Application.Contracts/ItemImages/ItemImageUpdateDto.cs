using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(ItemImageConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        [StringLength(ItemImageConsts.UrlMaxLength, MinimumLength = ItemImageConsts.UrlMinLength)]
        public string Url { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
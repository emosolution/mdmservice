using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(ItemAttachmentConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid ItemId { get; set; }
        [Required]
        public IRemoteStreamContent File { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
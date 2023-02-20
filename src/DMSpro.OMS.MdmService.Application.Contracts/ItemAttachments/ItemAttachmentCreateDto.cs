using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentCreateDto
    {
        [StringLength(ItemAttachmentConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public Guid ItemId { get; set; }
        [Required]
        public IRemoteStreamContent File { get; set; }
    }
}
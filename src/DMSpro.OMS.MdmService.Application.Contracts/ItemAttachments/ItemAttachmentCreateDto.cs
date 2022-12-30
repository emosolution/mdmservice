using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentCreateDto
    {
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        [Required]
        public string URL { get; set; }
        public Guid ItemId { get; set; }
    }
}
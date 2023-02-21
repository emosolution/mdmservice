using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentCreateDto
    {
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public Guid FileId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
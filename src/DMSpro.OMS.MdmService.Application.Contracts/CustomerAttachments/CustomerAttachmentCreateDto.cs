using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentCreateDto
    {
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        [Required]
        public IRemoteStreamContent File{ get; set; }
        [Required]
        public Guid CustomerId { get; set; }
    }
}
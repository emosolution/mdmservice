using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentUpdateDto : IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        [Required]
        public IRemoteStreamContent File { get; set; }
        [Required]
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
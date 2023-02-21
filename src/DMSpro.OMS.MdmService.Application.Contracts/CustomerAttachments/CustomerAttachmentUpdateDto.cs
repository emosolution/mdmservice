using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentUpdateDto : IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid FileId { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
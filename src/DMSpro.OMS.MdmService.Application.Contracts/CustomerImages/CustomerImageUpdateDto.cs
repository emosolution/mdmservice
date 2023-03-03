using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class CustomerImageUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(CustomerImageConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool IsAvatar { get; set; }
        public bool IsPOSM { get; set; }
        public Guid FileId { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
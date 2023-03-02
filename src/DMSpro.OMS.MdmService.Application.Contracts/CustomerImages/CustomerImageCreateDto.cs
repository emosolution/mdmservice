using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class CustomerImageCreateDto
    {
        [StringLength(CustomerImageConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public bool IsAvatar { get; set; } = false;
        public bool IsPOSM { get; set; } = false;
        public Guid FileId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
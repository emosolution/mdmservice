using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageUpdateDto : IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        [Required]
        public string URL { get; set; }
        public int DisplayOrder { get; set; }
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
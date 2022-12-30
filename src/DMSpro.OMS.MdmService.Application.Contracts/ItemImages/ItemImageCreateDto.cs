using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageCreateDto
    {
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        [Required]
        public string URL { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public Guid ItemId { get; set; }
    }
}
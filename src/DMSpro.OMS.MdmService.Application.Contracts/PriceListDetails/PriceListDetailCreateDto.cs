using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailCreateDto
    {
        public int Price { get; set; }
        public int? BasedOnPrice { get; set; }
        [Required]
        public string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid UOMId { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailCreateDto
    {
        public decimal Price { get; set; }
        public decimal? BasedOnPrice { get; set; }
        [Required]
        public string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid UOMId { get; set; }
        public Guid ItemId { get; set; }
    }
}
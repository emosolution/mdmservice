using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailUpdateDto : IHasConcurrencyStamp
    {
        public decimal Price { get; set; }
        public decimal? BasedOnPrice { get; set; }
        [Required]
        public string Description { get; set; }
        public Guid UOMId { get; set; }
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
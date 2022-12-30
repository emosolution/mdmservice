using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailUpdateDto : IHasConcurrencyStamp
    {
        public int Price { get; set; }
        public int? BasedOnPrice { get; set; }
        [Required]
        public string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid ItemMasterId { get; set; }
        public Guid UOMId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailCreateDto
    {
        public int PriceBeforeUpdate { get; set; }
        public int NewPrice { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid PriceUpdateId { get; set; }
        public Guid PriceListDetailId { get; set; }
    }
}
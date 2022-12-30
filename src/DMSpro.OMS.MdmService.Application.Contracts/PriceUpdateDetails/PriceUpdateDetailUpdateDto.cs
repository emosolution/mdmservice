using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailUpdateDto : IHasConcurrencyStamp
    {
        public int PriceBeforeUpdate { get; set; }
        public int NewPrice { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid PriceUpdateId { get; set; }
        public Guid PriceListDetailId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
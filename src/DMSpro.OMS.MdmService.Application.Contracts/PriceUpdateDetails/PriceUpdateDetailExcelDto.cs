using System;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailExcelDto
    {
        public int PriceBeforeUpdate { get; set; }
        public int NewPrice { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
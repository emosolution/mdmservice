using System;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailExcelDto
    {
        public decimal Price { get; set; }
        public decimal? BasedOnPrice { get; set; }
        public string Description { get; set; }
    }
}
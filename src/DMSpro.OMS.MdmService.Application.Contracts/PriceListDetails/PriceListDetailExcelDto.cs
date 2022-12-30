using System;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailExcelDto
    {
        public int Price { get; set; }
        public int? BasedOnPrice { get; set; }
        public string Description { get; set; }
    }
}
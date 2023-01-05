using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.UOMs;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailWithNavigationProperties
    {
        public PriceListDetail PriceListDetail { get; set; }

        public PriceList PriceList { get; set; }
        public UOM UOM { get; set; }
        

        
    }
}
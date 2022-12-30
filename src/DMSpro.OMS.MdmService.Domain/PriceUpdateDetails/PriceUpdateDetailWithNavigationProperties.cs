using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.PriceListDetails;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailWithNavigationProperties
    {
        public PriceUpdateDetail PriceUpdateDetail { get; set; }

        public PriceUpdate PriceUpdate { get; set; }
        public PriceListDetail PriceListDetail { get; set; }
        

        
    }
}
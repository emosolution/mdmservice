using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.Companies;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailWithNavigationProperties
    {
        public NumberingConfigDetail NumberingConfigDetail { get; set; }

        public NumberingConfig NumberingConfig { get; set; }
        public Company Company { get; set; }
        

        
    }
}
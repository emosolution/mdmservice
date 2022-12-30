using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SystemDatas;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigWithNavigationProperties
    {
        public NumberingConfig NumberingConfig { get; set; }

        public Company Company { get; set; }
        public SystemData SystemData { get; set; }
        

        
    }
}
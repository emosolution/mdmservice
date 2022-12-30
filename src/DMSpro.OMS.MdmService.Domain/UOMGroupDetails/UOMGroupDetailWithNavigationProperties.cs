using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMs;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetailWithNavigationProperties
    {
        public UOMGroupDetail UOMGroupDetail { get; set; }

        public UOMGroup UOMGroup { get; set; }
        public UOM AltUOM { get; set; }
        public UOM BaseUOM { get; set; }
        

        
    }
}
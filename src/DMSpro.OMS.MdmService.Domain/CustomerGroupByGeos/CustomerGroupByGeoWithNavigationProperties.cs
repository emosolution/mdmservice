using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.GeoMasters;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public class CustomerGroupByGeoWithNavigationProperties
    {
        public CustomerGroupByGeo CustomerGroupByGeo { get; set; }

        public CustomerGroup CustomerGroup { get; set; }
        public GeoMaster GeoMaster { get; set; }
        

        
    }
}
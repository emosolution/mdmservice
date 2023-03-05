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
        public GeoMaster GeoMaster0 { get; set; }
        public GeoMaster GeoMaster1 { get; set; }
        public GeoMaster GeoMaster2 { get; set; }
        public GeoMaster GeoMaster3 { get; set; }
        public GeoMaster GeoMaster4 { get; set; }
        

        
    }
}
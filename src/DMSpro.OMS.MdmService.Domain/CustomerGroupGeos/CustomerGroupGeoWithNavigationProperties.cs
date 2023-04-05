using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeoWithNavigationProperties
    {
        public CustomerGroupGeo CustomerGroupGeo { get; set; }

        public CustomerGroup CustomerGroup { get; set; }
        public GeoMaster GeoMaster { get; set; }
        public GeoMaster GeoMaster1 { get; set; }
        public GeoMaster GeoMaster2 { get; set; }
        public GeoMaster GeoMaster3 { get; set; }
        public GeoMaster GeoMaster4 { get; set; }
        

        
    }
}
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.GeoMasters;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompanyWithNavigationProperties
    {
        public Company Company { get; set; }

        public Company Company1 { get; set; }
        public GeoMaster GeoMaster { get; set; }
        public GeoMaster GeoMaster1 { get; set; }
        public GeoMaster GeoMaster2 { get; set; }
        public GeoMaster GeoMaster3 { get; set; }
        public GeoMaster GeoMaster4 { get; set; }
        

        
    }
}
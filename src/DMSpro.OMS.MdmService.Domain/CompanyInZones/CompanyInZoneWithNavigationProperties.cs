using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Companies;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public class CompanyInZoneWithNavigationProperties
    {
        public CompanyInZone CompanyInZone { get; set; }

        public SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public Company Company { get; set; }
        

        
    }
}
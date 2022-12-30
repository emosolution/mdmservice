using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Customers;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public class CustomerInZoneWithNavigationProperties
    {
        public CustomerInZone CustomerInZone { get; set; }

        public SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public Customer Customer { get; set; }
        

        
    }
}
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.ItemGroups;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class ItemGroupInZoneWithNavigationProperties
    {
        public ItemGroupInZone ItemGroupInZone { get; set; }

        public SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public ItemGroup ItemGroup { get; set; }
        

        
    }
}
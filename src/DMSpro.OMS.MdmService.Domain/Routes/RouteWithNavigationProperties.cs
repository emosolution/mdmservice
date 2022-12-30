using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Routes
{
    public class RouteWithNavigationProperties
    {
        public Route Route { get; set; }

        public SystemData SystemData { get; set; }
        public ItemGroup ItemGroup { get; set; }
        public SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        

        
    }
}
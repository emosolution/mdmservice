using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.ItemGroups;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeaderWithNavigationProperties
    {
        public MCPHeader MCPHeader { get; set; }

        public SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public Company Company { get; set; }
        public ItemGroup ItemGroup { get; set; }
        

        
    }
}
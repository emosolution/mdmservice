using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Companies;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeaderWithNavigationProperties
    {
        public MCPHeader MCPHeader { get; set; }

        public SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public Company Company { get; set; }
        

        
    }
}
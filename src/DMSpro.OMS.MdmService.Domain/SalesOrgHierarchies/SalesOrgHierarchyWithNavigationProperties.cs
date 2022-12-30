using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyWithNavigationProperties
    {
        public SalesOrgHierarchy SalesOrgHierarchy { get; set; }

        public SalesOrgHeader SalesOrgHeader { get; set; }
        public SalesOrgHierarchy SalesOrgHierarchy1 { get; set; }
        

        
    }
}
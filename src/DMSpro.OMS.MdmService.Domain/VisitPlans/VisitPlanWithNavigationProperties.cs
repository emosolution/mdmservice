using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.ItemGroups;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanWithNavigationProperties
    {
        public VisitPlan VisitPlan { get; set; }

        public MCPDetail MCPDetail { get; set; }
        public Customer Customer { get; set; }
        public SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public Company Company { get; set; }
        public ItemGroup ItemGroup { get; set; }
        

        
    }
}
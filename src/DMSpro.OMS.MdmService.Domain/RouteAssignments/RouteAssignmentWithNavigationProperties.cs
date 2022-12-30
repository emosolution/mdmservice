using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.EmployeeProfiles;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public class RouteAssignmentWithNavigationProperties
    {
        public RouteAssignment RouteAssignment { get; set; }

        public SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public EmployeeProfile EmployeeProfile { get; set; }
        

        
    }
}
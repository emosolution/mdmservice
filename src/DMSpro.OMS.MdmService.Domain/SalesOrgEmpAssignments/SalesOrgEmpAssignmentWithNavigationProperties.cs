using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.EmployeeProfiles;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentWithNavigationProperties
    {
        public SalesOrgEmpAssignment SalesOrgEmpAssignment { get; set; }

        public SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public EmployeeProfile EmployeeProfile { get; set; }
        

        
    }
}
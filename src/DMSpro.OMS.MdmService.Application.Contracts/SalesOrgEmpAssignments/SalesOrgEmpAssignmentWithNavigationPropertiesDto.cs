using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.EmployeeProfiles;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentWithNavigationPropertiesDto
    {
        public SalesOrgEmpAssignmentDto SalesOrgEmpAssignment { get; set; }

        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public EmployeeProfileDto EmployeeProfile { get; set; }

    }
}
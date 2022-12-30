using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.EmployeeProfiles;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public class EmployeeInZoneWithNavigationPropertiesDto
    {
        public EmployeeInZoneDto EmployeeInZone { get; set; }

        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public EmployeeProfileDto EmployeeProfile { get; set; }

    }
}
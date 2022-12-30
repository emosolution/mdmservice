using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Companies;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public class CompanyInZoneWithNavigationPropertiesDto
    {
        public CompanyInZoneDto CompanyInZone { get; set; }

        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public CompanyDto Company { get; set; }

    }
}
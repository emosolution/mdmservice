using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.ItemGroups;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeaderWithNavigationPropertiesDto
    {
        public MCPHeaderDto MCPHeader { get; set; }

        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public CompanyDto Company { get; set; }
        public ItemGroupDto ItemGroup { get; set; }

    }
}
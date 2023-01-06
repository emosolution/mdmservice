using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.ItemGroups;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanWithNavigationPropertiesDto
    {
        public VisitPlanDto VisitPlan { get; set; }

        public MCPDetailDto MCPDetail { get; set; }
        public CustomerDto Customer { get; set; }
        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public CompanyDto Company { get; set; }
        public ItemGroupDto ItemGroup { get; set; }

    }
}
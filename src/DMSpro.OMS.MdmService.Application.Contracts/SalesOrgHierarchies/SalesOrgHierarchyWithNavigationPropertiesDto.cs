using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyWithNavigationPropertiesDto
    {
        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }

        public SalesOrgHeaderDto SalesOrgHeader { get; set; }
        public SalesOrgHierarchyDto SalesOrgHierarchy1 { get; set; }

    }
}
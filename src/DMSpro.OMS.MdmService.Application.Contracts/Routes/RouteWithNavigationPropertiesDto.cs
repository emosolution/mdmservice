using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Routes
{
    public class RouteWithNavigationPropertiesDto
    {
        public RouteDto Route { get; set; }

        public SystemDataDto SystemData { get; set; }
        public ItemGroupDto ItemGroup { get; set; }
        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }

    }
}
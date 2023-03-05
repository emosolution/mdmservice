using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.ItemGroups;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class ItemGroupInZoneWithNavigationPropertiesDto
    {
        public ItemGroupInZoneDto ItemGroupInZone { get; set; }

        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public ItemGroupDto ItemGroup { get; set; }

    }
}
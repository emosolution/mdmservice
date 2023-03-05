using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.ItemGroups;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public partial class ItemGroupInZone 
    {
        public virtual SalesOrgHierarchy SellingZone { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }

        public Dictionary<string, (int, string, string, string)>
            GetExcelTemplateInfo()
        {
            return new()
            {
                { "SellingZoneId", (1, "ISalesOrgHierarchyRepository", "", "") },
                { "ItemGroupId", (1, "IItemGroupRepository", "", "") },
            };
        }

        public List<string> GetNotNullProperty()
        {
            return new();
        }
    }
}
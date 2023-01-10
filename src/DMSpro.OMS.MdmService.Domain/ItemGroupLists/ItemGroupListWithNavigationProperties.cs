using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.UOMs;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public class ItemGroupListWithNavigationProperties
    {
        public ItemGroupList ItemGroupList { get; set; }

        public ItemGroup ItemGroup { get; set; }
        public Item Item { get; set; }
        public UOM UOM { get; set; }
        

        
    }
}
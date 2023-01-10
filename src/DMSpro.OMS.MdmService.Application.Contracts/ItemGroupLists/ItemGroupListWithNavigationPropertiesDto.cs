using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.UOMs;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public class ItemGroupListWithNavigationPropertiesDto
    {
        public ItemGroupListDto ItemGroupList { get; set; }

        public ItemGroupDto ItemGroup { get; set; }
        public ItemDto Item { get; set; }
        public UOMDto UOM { get; set; }

    }
}
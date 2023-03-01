using DMSpro.OMS.MdmService.ItemGroups;
using System;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupExcelDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GroupType Type { get; set; }
        public GroupStatus Status { get; set; }
    }
}
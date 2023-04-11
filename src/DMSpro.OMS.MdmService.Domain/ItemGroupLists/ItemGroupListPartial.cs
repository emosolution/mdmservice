using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Items;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public partial class ItemGroupList
    {
        public virtual ItemGroup ItemGroup { get; set; }
        public virtual Item Item { get; set; }

        public Dictionary<string, (int, string, string, string)>
            GetExcelTemplateInfo()
        {
            return new()
            {
                { "ItemGroupId", (1, "IItemGroupRepository", "", "") },
                { "ItemId", (1, "IItemRepository", "", "") },
            };
        }

        public List<string> GetNotNullProperty()
        {
            return new();
        }
    }
}
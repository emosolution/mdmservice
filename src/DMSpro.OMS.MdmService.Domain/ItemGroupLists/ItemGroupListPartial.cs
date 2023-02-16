using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public partial class ItemGroupList
    {
        public Dictionary<string, (int, string, string, string)>
            GetExcelTemplateInfo()
        {
            return new()
            {
                { "ItemGroupId", (1, "IItemGroupRepository", "", "") },
                { "ItemId", (1, "IItemRepository", "", "") },
                { "Uom", (1, "IUOMRepository", "", "") },
            };
        }

        public List<string> GetNotNullProperty()
        {
            return new();
        }
    }
}
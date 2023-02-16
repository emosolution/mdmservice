using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public partial class ItemGroupAttribute
    {
        public Dictionary<string, (int, string, string, string)>
            GetExcelTemplateInfo()
        {
            return new()
            {
                { "ItemGroupId", (1, "IItemGroupRepository", "", "") },
                { "Attr0Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr1Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr2Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr3Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr4Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr5Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr6Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr7Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr8Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr9Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr10Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr11Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr12Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr13Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr14Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr15Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr16Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr17Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr18Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr19Id", (1, "IItemAttributeValueRepository", "", "") },
            };
        }

        public List<string> GetNotNullProperty()
        {
            return new()
            {
                "dummy",
            };
        }
    }
}
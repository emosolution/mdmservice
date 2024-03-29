using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.ItemGroups;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public partial class ItemGroupAttribute
    {
        public virtual ItemGroup ItemGroup { get; set; }
        public virtual ItemAttributeValue Attr0 { get; set; }
        public virtual ItemAttributeValue Attr1 { get; set; }
        public virtual ItemAttributeValue Attr2 { get; set; }
        public virtual ItemAttributeValue Attr3 { get; set; }
        public virtual ItemAttributeValue Attr4 { get; set; }
        public virtual ItemAttributeValue Attr5 { get; set; }
        public virtual ItemAttributeValue Attr6 { get; set; }
        public virtual ItemAttributeValue Attr7 { get; set; }
        public virtual ItemAttributeValue Attr8 { get; set; }
        public virtual ItemAttributeValue Attr9 { get; set; }
        public virtual ItemAttributeValue Attr10 { get; set; }
        public virtual ItemAttributeValue Attr11 { get; set; }
        public virtual ItemAttributeValue Attr12 { get; set; }
        public virtual ItemAttributeValue Attr13 { get; set; }
        public virtual ItemAttributeValue Attr14 { get; set; }
        public virtual ItemAttributeValue Attr15 { get; set; }
        public virtual ItemAttributeValue Attr16 { get; set; }
        public virtual ItemAttributeValue Attr17 { get; set; }
        public virtual ItemAttributeValue Attr18 { get; set; }
        public virtual ItemAttributeValue Attr19 { get; set; }

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
            return new();
        }
    }
}
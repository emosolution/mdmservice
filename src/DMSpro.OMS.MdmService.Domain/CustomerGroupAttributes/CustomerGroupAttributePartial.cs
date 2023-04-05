using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public partial class CustomerGroupAttribute
    {
        public virtual CustomerGroup CustomerGroup { get; set; }
        public virtual CustomerAttributeValue Attr0 { get; set; }
        public virtual CustomerAttributeValue Attr1 { get; set; }
        public virtual CustomerAttributeValue Attr2 { get; set; }
        public virtual CustomerAttributeValue Attr3 { get; set; }
        public virtual CustomerAttributeValue Attr4 { get; set; }
        public virtual CustomerAttributeValue Attr5 { get; set; }
        public virtual CustomerAttributeValue Attr6 { get; set; }
        public virtual CustomerAttributeValue Attr7 { get; set; }
        public virtual CustomerAttributeValue Attr8 { get; set; }
        public virtual CustomerAttributeValue Attr9 { get; set; }
        public virtual CustomerAttributeValue Attr10 { get; set; }
        public virtual CustomerAttributeValue Attr11 { get; set; }
        public virtual CustomerAttributeValue Attr12 { get; set; }
        public virtual CustomerAttributeValue Attr13 { get; set; }
        public virtual CustomerAttributeValue Attr14 { get; set; }
        public virtual CustomerAttributeValue Attr15 { get; set; }
        public virtual CustomerAttributeValue Attr16 { get; set; }
        public virtual CustomerAttributeValue Attr17 { get; set; }
        public virtual CustomerAttributeValue Attr18 { get; set; }
        public virtual CustomerAttributeValue Attr19 { get; set; }

        public Dictionary<string, (int, string, string, string)>
            GetExcelTemplateInfo()
        {
            return new()
            {
                { "CustomerGroupId", (1, "ICustomerGroupRepository", "", "") },
                { "Attr0Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr1Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr2Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr3Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr4Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr5Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr6Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr7Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr8Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr9Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr10Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr11Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr12Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr13Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr14Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr15Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr16Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr17Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr18Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr19Id", (1, "ICustomerAttributeValueRepository", "", "") },
            };
        }

        public List<string> GetNotNullProperty()
        {
            return new();
        }
    }
}
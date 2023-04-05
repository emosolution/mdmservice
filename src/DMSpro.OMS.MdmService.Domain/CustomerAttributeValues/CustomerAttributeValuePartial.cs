using System.Collections.Generic;
using DMSpro.OMS.MdmService.CustomerAttributes;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public partial class CustomerAttributeValue 
    {
        public virtual CustomerAttributeValue Parent { get; set; }
        public virtual CustomerAttribute CustomerAttribute { get; set; }

        public Dictionary<string, (int, string, string, string)>
            GetExcelTemplateInfo()
        {
            return new()
            {
                { "CustomerAttributeId", (1, "ICustomerAttributeRepository", "", "") },
                { "ParentId", (0, "ICustomerAttributeValueRepository", "", "") },
            };
        }

        public List<string> GetNotNullProperty()
        {
            return new()
            {
                "Code",
                "AttrValName",
            };
        }
    }
}
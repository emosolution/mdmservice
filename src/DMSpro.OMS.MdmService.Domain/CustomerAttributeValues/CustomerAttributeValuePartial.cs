using System.Collections.Generic;
using DMSpro.OMS.MdmService.CustomerAttributes;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public partial class CustomerAttributeValue 
    {
        public virtual CustomerAttribute CustomerAttribute { get; set; }

        public Dictionary<string, (int, string, string, string)>
            GetExcelTemplateInfo()
        {
            return new()
            {
                { "CustomerAttributeId", (1, "ICustomerAttributeRepository", "", "") },
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
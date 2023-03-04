using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.Items;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public partial class CustomerImage
    {
        public Dictionary<string, (int, string, string, string)>
            GetExcelTemplateInfo()
        {
            return new()
            {
                { "CustomerId", (0, "ICustomerRepository", "", "") },
                { "ItemPOSMId", (0, "IItemRepository", "", "") },
            };
        }

        public List<string> GetNotNullProperty()
        {
            return new();
        }

        public virtual Customer Customer { get; set; }
        public virtual Item ItemPOSM { get; set; }
    }
}
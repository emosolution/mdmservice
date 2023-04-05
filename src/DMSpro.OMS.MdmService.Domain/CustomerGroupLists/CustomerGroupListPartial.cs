using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.CustomerGroups;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public partial class CustomerGroupList
    {
        public virtual Customer Customer { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }

        public Dictionary<string, (int, string, string, string)>
            GetExcelTemplateInfo()
        {
            return new()
            {
                { "CustomerGroupId", (1, "ICustomerGroupRepository", "", "") },
                { "CustomerId", (1, "ICustomerRepository", "", "") },
            };
        }

        public List<string> GetNotNullProperty()
        {
            return new();
        }
    }
}
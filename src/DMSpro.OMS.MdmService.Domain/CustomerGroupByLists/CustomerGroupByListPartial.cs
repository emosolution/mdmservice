using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
	public partial class CustomerGroupByList
	{
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
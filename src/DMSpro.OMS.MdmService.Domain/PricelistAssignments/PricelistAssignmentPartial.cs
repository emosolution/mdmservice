using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
	public partial class PricelistAssignment
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "PriceListId", (1, "IPriceListRepository", "", "") },
                { "CustomerGroupId", (1, "ICustomerGroupRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
	public partial class CustomerInZone
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "SalesOrgHierarchyId", (1, "ISalesOrgHierarchyRepository", "", "") },
                { "CustomerId", (1, "ICustomerRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
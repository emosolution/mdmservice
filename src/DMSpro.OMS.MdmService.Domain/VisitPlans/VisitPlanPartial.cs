using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.VisitPlans
{
	public partial class VisitPlan
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "MCPDetailId", (1, "IMCPDetailRepository", "", "") },
                { "CustomerId", (1, "ICustomerRepository", "", "") },
                { "RouteId", (1, "ISalesOrgHierarchyRepository", "", "") },
                { "CompanyId", (1, "ICompanyRepository", "", "") },
                { "ItemGroupId", (1, "IItemGroupRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
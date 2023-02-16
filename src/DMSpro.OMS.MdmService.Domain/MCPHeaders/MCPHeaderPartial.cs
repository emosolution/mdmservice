using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
	public partial class MCPHeader
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "RouteId", (1, "ISalesOrgHierarchyRepository", "", "") },
                { "CompanyId", (1, "ICompanyRepository", "", "") },
                { "ItemGroupId", (1, "IItemGroupRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "Code",
            };
        }
    }
}
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
	public partial class SalesOrgHierarchy
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "ParentId", (0, "ISalesOrgHierarchyRepository", "", "") },
                { "SalesOrgHeaderId", (1, "ISalesOrgHeaderRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "Code",
                "HierarchyCode",
            };
        }
    }
}
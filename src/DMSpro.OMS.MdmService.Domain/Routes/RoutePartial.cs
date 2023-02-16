using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Routes
{
	public partial class Route
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "RouteTypeId", (1, "ISystemDataRepository", "", "") },
                { "ItemGroupId", (1, "IItemGroupRepository", "", "") },
                { "SalesOrgHierarchyId", (1, "ISalesOrgHierarchyRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
            return new();
        }
    }
}
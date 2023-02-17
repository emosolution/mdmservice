using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
	public partial class RouteAssignment
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "RouteId", (1, "ISalesOrgHierarchyRepository", "", "") },
                { "EmployeeId", (1, "IEmployeeProfileRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
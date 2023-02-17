using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
	public partial class EmployeeInZone
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "SalesOrgHierarchyId", (1, "ISalesOrgHierarchyRepository", "", "") },
                { "EmployeeId", (1, "IEmployeeProfileRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
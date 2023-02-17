using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
	public partial class SalesOrgEmpAssignment
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "SalesOrgHierarchyId", (1, "ISalesOrgHierarchyRepository", "", "") },
                { "EmployeeProfileId", (1, "IEmployeeProfileRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
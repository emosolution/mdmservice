using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
	public partial class SalesOrgEmpAssignment
	{
        public virtual SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public virtual EmployeeProfile EmployeeProfile { get; set; }

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
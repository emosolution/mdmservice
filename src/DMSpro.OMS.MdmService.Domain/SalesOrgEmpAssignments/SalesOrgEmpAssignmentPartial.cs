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
				{ "", (0, "IRepository", "", "") },
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
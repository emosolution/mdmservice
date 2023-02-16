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
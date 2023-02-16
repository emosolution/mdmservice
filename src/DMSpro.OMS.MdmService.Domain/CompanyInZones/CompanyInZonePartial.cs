using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
	public partial class CompanyInZone
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "SalesOrgHierarchyId", (1, "ISalesOrgHierarchyRepository", "", "") },
                { "CompanyId", (1, "ICompanyRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
	public partial class CompanyInZone
	{
        public virtual Company Company { get; set; }
        public virtual SalesOrgHierarchy SalesOrgHierarchy { get; set; }

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
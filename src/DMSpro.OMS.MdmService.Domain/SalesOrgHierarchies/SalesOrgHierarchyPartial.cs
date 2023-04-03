using DMSpro.OMS.MdmService.SalesOrgHeaders;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
	public partial class SalesOrgHierarchy
	{
        public virtual SalesOrgHierarchy Parent { get; set; }
        public virtual SalesOrgHeader SalesOrgHeader { get; set; }


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
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.VisitPlans
{
	public partial class VisitPlan
	{
        public virtual MCPDetail MCPDetail { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual SalesOrgHierarchy Route { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }

        public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "MCPDetailId", (1, "IMCPDetailRepository", "", "") },
                { "CustomerId", (1, "ICustomerRepository", "", "") },
                { "RouteId", (1, "ISalesOrgHierarchyRepository", "", "") },
                { "ItemGroupId", (1, "IItemGroupRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
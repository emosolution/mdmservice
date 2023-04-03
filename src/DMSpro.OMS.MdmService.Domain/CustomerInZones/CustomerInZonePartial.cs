using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
	public partial class CustomerInZone
	{
        public virtual Customer Customer { get; set; }
        public virtual SalesOrgHierarchy SalesOrgHierarchy { get; set; }

        public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "SalesOrgHierarchyId", (1, "ISalesOrgHierarchyRepository", "", "") },
                { "CustomerId", (1, "ICustomerRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
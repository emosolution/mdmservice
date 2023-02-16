using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
	public partial class CustomerGroupByGeo
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "CustomerGroupId", (1, "ICustomerGroupRepository", "", "") },
                { "GeoMasterId", (1, "IGeoMasterRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
	public partial class CustomerGroupByAtt
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "CustomerGroupId", (1, "ICustomerGroupRepository", "", "") },
                { "CusAttributeValueId", (1, "ICusAttributeValueRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
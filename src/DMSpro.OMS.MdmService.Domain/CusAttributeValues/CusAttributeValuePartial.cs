using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
	public partial class CusAttributeValue
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "CustomerAttributeId", (1, "ICustomerAttributeRepository", "", "") },
                { "ParentCusAttributeValueId", (0, "ICusAttributeValueRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "AttrValName",
            };
        }
    }
}
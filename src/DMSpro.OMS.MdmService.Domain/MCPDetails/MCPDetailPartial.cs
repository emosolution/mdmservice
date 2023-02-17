using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPDetails
{
	public partial class MCPDetail
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "CustomerId", (1, "ICustomerRepository", "", "") },
                { "MCPHeaderId", (1, "IMCPHeaderRepository", "", "") },
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
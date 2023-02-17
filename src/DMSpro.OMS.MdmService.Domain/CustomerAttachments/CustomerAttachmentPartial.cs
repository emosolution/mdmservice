using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
	public partial class CustomerAttachment
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "CustomerId", (0, "ICustomerRepository", "", "") },
			};
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "url",
            };
        }
    }
}
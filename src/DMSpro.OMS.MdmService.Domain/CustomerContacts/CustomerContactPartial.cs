using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
	public partial class CustomerContact
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "CustomerId", (1, "ICustomerRepository", "", "") },
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
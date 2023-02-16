using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
	public partial class CustomerAssignment
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "CompanyId", (1, "ICompanyRepository", "", "") },
                { "CustomerId", (1, "ICustomerRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
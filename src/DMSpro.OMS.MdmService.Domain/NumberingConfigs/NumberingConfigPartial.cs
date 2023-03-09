using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
	public partial class NumberingConfig
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "CompanyId", (1, "ICompanyRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
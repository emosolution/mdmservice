using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
	public partial class CustomerGroup
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new();
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
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
	public partial class SystemConfig
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
                "Description",
				"Value",
                "DefaultValue",

            };
        }
    }
}
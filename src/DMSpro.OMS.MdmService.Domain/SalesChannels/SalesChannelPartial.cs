using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesChannels
{
	public partial class SalesChannel
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
				"Name",
            };
        }
    }
}
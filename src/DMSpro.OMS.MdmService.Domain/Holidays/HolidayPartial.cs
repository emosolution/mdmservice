using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Holidays
{
	public partial class Holiday
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
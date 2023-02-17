using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Streets
{
	public partial class Street
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
                "Name",
            };
        }
    }
}
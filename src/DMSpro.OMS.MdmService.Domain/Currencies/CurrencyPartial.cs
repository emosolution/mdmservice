using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Currencies
{
	public partial class Currency
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
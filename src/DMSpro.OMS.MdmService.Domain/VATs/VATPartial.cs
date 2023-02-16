using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.VATs
{
	public partial class VAT
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
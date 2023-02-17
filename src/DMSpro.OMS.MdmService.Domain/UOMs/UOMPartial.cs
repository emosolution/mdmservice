using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.UOMs
{
	public partial class UOM
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
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SystemDatas
{
	public partial class SystemData
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
                "ValueCode",
                "ValueName",
            };
        }
    }
}
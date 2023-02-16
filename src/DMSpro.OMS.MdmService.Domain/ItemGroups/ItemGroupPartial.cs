using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroups
{
	public partial class ItemGroup
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
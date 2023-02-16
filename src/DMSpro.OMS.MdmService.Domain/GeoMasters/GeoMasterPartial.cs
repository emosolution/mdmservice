using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.GeoMasters
{
	public partial class GeoMaster
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "ParentId", (0, "IGeoMasterRepository", "", "") },
			};
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
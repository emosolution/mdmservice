using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
	public partial class HolidayDetail
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "HolidayId", (1, "IHolidayRepository", "", "") },
			};
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}
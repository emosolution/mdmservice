using DMSpro.OMS.MdmService.Holidays;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
	public partial class HolidayDetail
	{
        public virtual Holiday Holiday { get; set; }

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
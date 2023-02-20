using System;
using DMSpro.OMS.MdmService.Holidays;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
	public class HolidayDetailWithDetailsDto
	{
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public Guid HolidayId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public HolidayDto Holiday { get; set; }
        public HolidayDetailWithDetailsDto()
		{
		}
	}
}


using DMSpro.OMS.MdmService.Holidays;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.HolidayDetails
{
	public class HolidayDetailWithDetailsDto: HolidayDetailDto, IHasConcurrencyStamp
	{
        public HolidayDto Holiday { get; set; }
        public HolidayDetailWithDetailsDto()
		{
		}
	}
}


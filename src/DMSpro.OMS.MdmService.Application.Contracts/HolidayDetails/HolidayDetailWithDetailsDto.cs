using System;
using DMSpro.OMS.MdmService.Holidays;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.HolidayDetails
{
	public class HolidayDetailWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
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


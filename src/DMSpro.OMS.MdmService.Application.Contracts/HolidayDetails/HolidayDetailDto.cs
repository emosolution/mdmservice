using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public Guid HolidayId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
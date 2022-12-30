using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class HolidayDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int Year { get; set; }
        public string Description { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
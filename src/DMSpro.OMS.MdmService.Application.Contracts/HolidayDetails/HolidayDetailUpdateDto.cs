using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailUpdateDto : IHasConcurrencyStamp
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public Guid HolidayId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
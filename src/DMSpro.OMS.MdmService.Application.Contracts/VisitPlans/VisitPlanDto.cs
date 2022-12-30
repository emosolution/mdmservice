using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime DateVisit { get; set; }
        public int Distance { get; set; }
        public int VisitOrder { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public DayOfWeek DayOfWeek { get; set; }
        public int Week { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Guid MCPDetailId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanCreateDto
    {
        public DateTime DateVisit { get; set; }
        public int Distance { get; set; } = 0;
        public int VisitOrder { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public DayOfWeek DayOfWeek { get; set; } = ((DayOfWeek[])Enum.GetValues(typeof(DayOfWeek)))[0];
        public int Week { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Guid MCPDetailId { get; set; }
    }
}
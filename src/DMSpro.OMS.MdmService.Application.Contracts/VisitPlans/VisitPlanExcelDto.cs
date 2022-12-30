using System;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanExcelDto
    {
        public DateTime DateVisit { get; set; }
        public int Distance { get; set; }
        public int VisitOrder { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public DayOfWeek DayOfWeek { get; set; }
        public int Week { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
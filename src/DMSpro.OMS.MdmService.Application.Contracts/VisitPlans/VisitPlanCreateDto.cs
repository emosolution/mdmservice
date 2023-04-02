using System;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanCreateDto
    {
        public DateTime DateVisit { get; set; }
        public int Distance { get; set; }
        public int VisitOrder { get; set; }
        public Guid MCPDetailId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RouteId { get; set; }
        public Guid? ItemGroupId { get; set; }
    }
}
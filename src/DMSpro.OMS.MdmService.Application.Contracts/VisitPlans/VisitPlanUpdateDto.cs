using DMSpro.OMS.MdmService.VisitPlans;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanUpdateDto : IHasConcurrencyStamp
    {
        public DateTime DateVisit { get; set; }
        public int Distance { get; set; }
        public int VisitOrder { get; set; }
        public Guid MCPDetailId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RouteId { get; set; }
        public Guid? ItemGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
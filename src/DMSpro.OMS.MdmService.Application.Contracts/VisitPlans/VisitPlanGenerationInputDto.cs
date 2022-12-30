using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanGenerationInputDto
    {
        public Guid MCPHeaderId { get; set; }
        public List<Guid> MCPDetailIds { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        
        public VisitPlanGenerationInputDto()
        {

        }
    }
}
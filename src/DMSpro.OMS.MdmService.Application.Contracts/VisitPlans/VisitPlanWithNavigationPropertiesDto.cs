using DMSpro.OMS.MdmService.MCPDetails;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanWithNavigationPropertiesDto
    {
        public VisitPlanDto VisitPlan { get; set; }

        public MCPDetailDto MCPDetail { get; set; }

    }
}
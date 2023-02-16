using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.VisitPlans
{
	public partial interface IVisitPlanRepository
	{
		Task<List<VisitPlan>> GetByIdAsync(List<Guid> ids);
    }
}
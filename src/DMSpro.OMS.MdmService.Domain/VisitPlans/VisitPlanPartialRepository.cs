using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.VisitPlans
{
	public partial interface IVisitPlanRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}
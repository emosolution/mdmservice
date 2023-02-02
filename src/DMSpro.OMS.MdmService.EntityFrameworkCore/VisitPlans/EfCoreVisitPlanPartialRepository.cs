using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.VisitPlans
{
	public partial class EfCoreVisitPlanRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
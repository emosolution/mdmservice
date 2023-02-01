using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
	public partial class EfCoreRouteAssignmentRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
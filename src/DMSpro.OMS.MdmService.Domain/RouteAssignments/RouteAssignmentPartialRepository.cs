using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
	public partial interface IRouteAssignmentRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}
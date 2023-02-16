using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
	public partial interface IRouteAssignmentRepository
	{
		Task<List<RouteAssignment>> GetByIdAsync(List<Guid> ids);
    }
}
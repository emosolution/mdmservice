using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
	public partial interface IPricelistAssignmentRepository
	{
		Task<List<PricelistAssignment>> GetByIdAsync(List<Guid> ids);
    }
}
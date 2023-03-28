using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
	public partial interface IPricelistAssignmentRepository : IRepository<PricelistAssignment, Guid>
    {
		Task<List<PricelistAssignment>> GetByIdAsync(List<Guid> ids);
    }
}
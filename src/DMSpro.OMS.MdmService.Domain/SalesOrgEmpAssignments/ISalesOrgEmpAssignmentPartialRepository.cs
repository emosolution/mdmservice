using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
	public partial interface ISalesOrgEmpAssignmentRepository : IRepository<SalesOrgEmpAssignment, Guid>
    {
		Task<List<SalesOrgEmpAssignment>> GetByIdAsync(List<Guid> ids);
    }
}
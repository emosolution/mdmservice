using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
	public partial interface ISalesOrgEmpAssignmentRepository
	{
		Task<List<SalesOrgEmpAssignment>> GetByIdAsync(List<Guid> ids);
    }
}
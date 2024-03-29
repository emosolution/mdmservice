using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
	public partial interface ICustomerAssignmentRepository
	{
		Task<List<CustomerAssignment>> GetByIdAsync(List<Guid> ids);
    }
}
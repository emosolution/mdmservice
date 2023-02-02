using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
	public partial interface ISalesOrgEmpAssignmentRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}
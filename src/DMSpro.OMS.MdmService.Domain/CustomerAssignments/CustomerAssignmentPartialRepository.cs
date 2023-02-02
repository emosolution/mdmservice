using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
	public partial interface ICustomerAssignmentRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}
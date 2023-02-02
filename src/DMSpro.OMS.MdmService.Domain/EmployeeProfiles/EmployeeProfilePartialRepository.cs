using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
	public partial interface IEmployeeProfileRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}
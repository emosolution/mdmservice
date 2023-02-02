using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
	public partial interface ICompanyIdentityUserAssignmentRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}
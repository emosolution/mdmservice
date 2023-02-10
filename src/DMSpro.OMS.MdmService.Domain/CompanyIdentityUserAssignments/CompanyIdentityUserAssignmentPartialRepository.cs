using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
	public partial interface ICompanyIdentityUserAssignmentRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);

		Task<IQueryable<CompanyIdentityUserAssignmentWithNavigationProperties>> GetQueryAbleForNavigationPropertiesAsync(Guid? userId);
		
	}
}
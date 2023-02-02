using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
	public partial class EfCoreCompanyIdentityUserAssignmentRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IQueryable<CompanyIdentityUserAssignmentWithNavigationProperties>> GetQueryAbleForNavigationPropertiesAsync(Guid userId)
        {
            return from companyIdentityUserAssignment in (await GetDbSetAsync())
                   join company in (await GetDbContextAsync()).Companies on companyIdentityUserAssignment.CompanyId equals company.Id into companies
                   from company in companies.DefaultIfEmpty()
                   where companyIdentityUserAssignment.IdentityUserId == userId
                   select new CompanyIdentityUserAssignmentWithNavigationProperties
                   {
                       CompanyIdentityUserAssignment = companyIdentityUserAssignment,
                       Company = company
                   };
        }
    }
}
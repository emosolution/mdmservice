using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial class EfCoreCompanyIdentityUserAssignmentRepository
    {
        public virtual async Task<IQueryable<CompanyIdentityUserAssignmentWithNavigationProperties>>
            GetQueryAbleForNavigationPropertiesAsync(Guid? userId)
        {
            if (userId != null)
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
            else
            {
                return from companyIdentityUserAssignment in (await GetDbSetAsync())
                       join company in (await GetDbContextAsync()).Companies on companyIdentityUserAssignment.CompanyId equals company.Id into companies
                       from company in companies.DefaultIfEmpty()
                       select new CompanyIdentityUserAssignmentWithNavigationProperties
                       {
                           CompanyIdentityUserAssignment = companyIdentityUserAssignment,
                           Company = company
                       };
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;

namespace DMSpro.OMS.MdmService.Companies
{
    public class EfCoreCompanyCustomRepository : EfCoreRepository<MdmServiceDbContext, Company, Guid>, ICompanyCustomRepository
    {
        ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;

        public EfCoreCompanyCustomRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider, 
            ICompanyIdentityUserAssignmentRepository companyIdentityUserAssignmentRepository)
            : base(dbContextProvider)
        {
            _companyIdentityUserAssignmentRepository = companyIdentityUserAssignmentRepository;
        }

        public async Task<Company> GetHOCompanyOfTenant(Guid? tenantId)
        {
            var dbContext = await GetDbContextAsync();
            List<Company> companies = dbContext.Companies.Where(c => c.TenantId == tenantId && c.IsHO == true).ToList();
            string tenantIdString = tenantId != null ? tenantId.ToString() : "null";
            if (companies.Count < 1)
            {
                //Console.WriteLine($"No HO company found for tenant with Id {tenantIdString}.");
                return null;
            }
            if (companies.Count > 1)
            {
                //Console.WriteLine($"More than 1 HO company found for tenant with Id {tenantIdString}.");
                return null;
            }
            Company companyHO = companies.First();
            return companyHO;
        }

        public async Task<Company> GetHOCompanyFromIdentityUser(Guid identityUser, Guid? tenantId)
        {
            Company companyHO = await GetHOCompanyOfTenant(tenantId);
            if (companyHO == null)
            {
                return null;
            }
            long count = await _companyIdentityUserAssignmentRepository.GetCountAsync(identityUserId: identityUser, companyId: companyHO.Id);
            if (count != 1)
            {
                return null;
            }
            return companyHO;
        }
    }
}

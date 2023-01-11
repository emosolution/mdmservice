using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Volo.Abp;
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

        public async Task<Company> FindHOCompanyOfTenant(Guid tenantId)
        {
            var dbContext = await GetDbContextAsync();
            List<Company> companies = dbContext.Companies.Where(c => c.TenantId == tenantId && c.IsHO == true).ToList();
            if (companies.Count > 1)
            {
                throw new BusinessException(code: "550", message: $"No HO company found for tenant with Id {tenantId}.");
            }
            if (companies.Count < 1)
            {
                throw new BusinessException(code: "551", message: $"More than  HO company found for tenant with Id {tenantId}.");
            }
            Company companyHO = companies.First();
            return companyHO;
        }

        public async Task<Company> FindHOCompanyOfIdentityUser(Guid identityUser, Guid tenantId)
        {
            Company companyHO = await FindHOCompanyOfTenant(tenantId);
            long count = await _companyIdentityUserAssignmentRepository.GetCountAsync(identityUserId: identityUser, companyId: companyHO.Id);
            if (count == 1)
            {
                return companyHO;
            }
            return null;
        }
    }
}

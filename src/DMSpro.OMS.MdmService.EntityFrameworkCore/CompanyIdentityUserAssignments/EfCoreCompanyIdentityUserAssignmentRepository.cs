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

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial class EfCoreCompanyIdentityUserAssignmentRepository : EfCoreRepository<MdmServiceDbContext, CompanyIdentityUserAssignment, Guid>, ICompanyIdentityUserAssignmentRepository
    {
        public EfCoreCompanyIdentityUserAssignmentRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CompanyIdentityUserAssignmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(companyIdentityUserAssignment => new CompanyIdentityUserAssignmentWithNavigationProperties
                {
                    CompanyIdentityUserAssignment = companyIdentityUserAssignment,
                    Company = dbContext.Companies.FirstOrDefault(c => c.Id == companyIdentityUserAssignment.CompanyId)
                }).FirstOrDefault();
        }

        public async Task<List<CompanyIdentityUserAssignmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            Guid? identityUserId = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, identityUserId, companyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyIdentityUserAssignmentConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CompanyIdentityUserAssignmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
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

        protected virtual IQueryable<CompanyIdentityUserAssignmentWithNavigationProperties> ApplyFilter(
            IQueryable<CompanyIdentityUserAssignmentWithNavigationProperties> query,
            string filterText,
            Guid? identityUserId = null,
            Guid? companyId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(identityUserId.HasValue, e => e.CompanyIdentityUserAssignment.IdentityUserId == identityUserId)
                    .WhereIf(companyId != null && companyId != Guid.Empty, e => e.Company != null && e.Company.Id == companyId);
        }

        public async Task<List<CompanyIdentityUserAssignment>> GetListAsync(
            string filterText = null,
            Guid? identityUserId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, identityUserId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyIdentityUserAssignmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            Guid? identityUserId = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, identityUserId, companyId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CompanyIdentityUserAssignment> ApplyFilter(
            IQueryable<CompanyIdentityUserAssignment> query,
            string filterText,
            Guid? identityUserId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(identityUserId.HasValue, e => e.IdentityUserId == identityUserId);
        }
    }
}
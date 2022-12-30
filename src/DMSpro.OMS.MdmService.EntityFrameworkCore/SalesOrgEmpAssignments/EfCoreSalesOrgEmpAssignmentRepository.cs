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

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class EfCoreSalesOrgEmpAssignmentRepository : EfCoreRepository<MdmServiceDbContext, SalesOrgEmpAssignment, Guid>, ISalesOrgEmpAssignmentRepository
    {
        public EfCoreSalesOrgEmpAssignmentRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<SalesOrgEmpAssignmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(salesOrgEmpAssignment => new SalesOrgEmpAssignmentWithNavigationProperties
                {
                    SalesOrgEmpAssignment = salesOrgEmpAssignment,
                    SalesOrgHierarchy = dbContext.SalesOrgHierarchies.FirstOrDefault(c => c.Id == salesOrgEmpAssignment.SalesOrgHierarchyId),
                    EmployeeProfile = dbContext.EmployeeProfiles.FirstOrDefault(c => c.Id == salesOrgEmpAssignment.EmployeeProfileId)
                }).FirstOrDefault();
        }

        public async Task<List<SalesOrgEmpAssignmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? isBase = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? employeeProfileId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, isBase, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, salesOrgHierarchyId, employeeProfileId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SalesOrgEmpAssignmentConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<SalesOrgEmpAssignmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from salesOrgEmpAssignment in (await GetDbSetAsync())
                   join salesOrgHierarchy in (await GetDbContextAsync()).SalesOrgHierarchies on salesOrgEmpAssignment.SalesOrgHierarchyId equals salesOrgHierarchy.Id into salesOrgHierarchies
                   from salesOrgHierarchy in salesOrgHierarchies.DefaultIfEmpty()
                   join employeeProfile in (await GetDbContextAsync()).EmployeeProfiles on salesOrgEmpAssignment.EmployeeProfileId equals employeeProfile.Id into employeeProfiles
                   from employeeProfile in employeeProfiles.DefaultIfEmpty()

                   select new SalesOrgEmpAssignmentWithNavigationProperties
                   {
                       SalesOrgEmpAssignment = salesOrgEmpAssignment,
                       SalesOrgHierarchy = salesOrgHierarchy,
                       EmployeeProfile = employeeProfile
                   };
        }

        protected virtual IQueryable<SalesOrgEmpAssignmentWithNavigationProperties> ApplyFilter(
            IQueryable<SalesOrgEmpAssignmentWithNavigationProperties> query,
            string filterText,
            bool? isBase = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? employeeProfileId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(isBase.HasValue, e => e.SalesOrgEmpAssignment.IsBase == isBase)
                    .WhereIf(effectiveDateMin.HasValue, e => e.SalesOrgEmpAssignment.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.SalesOrgEmpAssignment.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.SalesOrgEmpAssignment.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.SalesOrgEmpAssignment.EndDate <= endDateMax.Value)
                    .WhereIf(salesOrgHierarchyId != null && salesOrgHierarchyId != Guid.Empty, e => e.SalesOrgHierarchy != null && e.SalesOrgHierarchy.Id == salesOrgHierarchyId)
                    .WhereIf(employeeProfileId != null && employeeProfileId != Guid.Empty, e => e.EmployeeProfile != null && e.EmployeeProfile.Id == employeeProfileId);
        }

        public async Task<List<SalesOrgEmpAssignment>> GetListAsync(
            string filterText = null,
            bool? isBase = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, isBase, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SalesOrgEmpAssignmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            bool? isBase = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? employeeProfileId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, isBase, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, salesOrgHierarchyId, employeeProfileId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<SalesOrgEmpAssignment> ApplyFilter(
            IQueryable<SalesOrgEmpAssignment> query,
            string filterText,
            bool? isBase = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(isBase.HasValue, e => e.IsBase == isBase)
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value);
        }
    }
}
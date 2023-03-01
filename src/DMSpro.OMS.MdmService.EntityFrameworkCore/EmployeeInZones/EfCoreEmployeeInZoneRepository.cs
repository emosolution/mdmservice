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

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public partial class EfCoreEmployeeInZoneRepository : EfCoreRepository<MdmServiceDbContext, EmployeeInZone, Guid>, IEmployeeInZoneRepository
    {
        public EfCoreEmployeeInZoneRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<EmployeeInZoneWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(employeeInZone => new EmployeeInZoneWithNavigationProperties
                {
                    EmployeeInZone = employeeInZone,
                    SalesOrgHierarchy = dbContext.SalesOrgHierarchies.FirstOrDefault(c => c.Id == employeeInZone.SalesOrgHierarchyId),
                    EmployeeProfile = dbContext.EmployeeProfiles.FirstOrDefault(c => c.Id == employeeInZone.EmployeeId)
                }).FirstOrDefault();
        }

        public async Task<List<EmployeeInZoneWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? employeeId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, salesOrgHierarchyId, employeeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeInZoneConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<EmployeeInZoneWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from employeeInZone in (await GetDbSetAsync())
                   join salesOrgHierarchy in (await GetDbContextAsync()).SalesOrgHierarchies on employeeInZone.SalesOrgHierarchyId equals salesOrgHierarchy.Id into salesOrgHierarchies
                   from salesOrgHierarchy in salesOrgHierarchies.DefaultIfEmpty()
                   join employeeProfile in (await GetDbContextAsync()).EmployeeProfiles on employeeInZone.EmployeeId equals employeeProfile.Id into employeeProfiles
                   from employeeProfile in employeeProfiles.DefaultIfEmpty()

                   select new EmployeeInZoneWithNavigationProperties
                   {
                       EmployeeInZone = employeeInZone,
                       SalesOrgHierarchy = salesOrgHierarchy,
                       EmployeeProfile = employeeProfile
                   };
        }

        protected virtual IQueryable<EmployeeInZoneWithNavigationProperties> ApplyFilter(
            IQueryable<EmployeeInZoneWithNavigationProperties> query,
            string filterText,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? employeeId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(effectiveDateMin.HasValue, e => e.EmployeeInZone.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EmployeeInZone.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EmployeeInZone.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EmployeeInZone.EndDate <= endDateMax.Value)
                    .WhereIf(salesOrgHierarchyId != null && salesOrgHierarchyId != Guid.Empty, e => e.SalesOrgHierarchy != null && e.SalesOrgHierarchy.Id == salesOrgHierarchyId)
                    .WhereIf(employeeId != null && employeeId != Guid.Empty, e => e.EmployeeProfile != null && e.EmployeeProfile.Id == employeeId);
        }

        public async Task<List<EmployeeInZone>> GetListAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeInZoneConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, salesOrgHierarchyId, employeeId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<EmployeeInZone> ApplyFilter(
            IQueryable<EmployeeInZone> query,
            string filterText,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value);
        }
    }
}
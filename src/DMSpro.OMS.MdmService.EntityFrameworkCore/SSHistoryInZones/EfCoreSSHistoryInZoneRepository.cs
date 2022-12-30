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

namespace DMSpro.OMS.MdmService.SSHistoryInZones
{
    public class EfCoreSSHistoryInZoneRepository : EfCoreRepository<MdmServiceDbContext, SSHistoryInZone, Guid>, ISSHistoryInZoneRepository
    {
        public EfCoreSSHistoryInZoneRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<SSHistoryInZoneWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(ssHistoryInZone => new SSHistoryInZoneWithNavigationProperties
                {
                    SSHistoryInZone = ssHistoryInZone,
                    SalesOrgHierarchy = dbContext.SalesOrgHierarchies.FirstOrDefault(c => c.Id == ssHistoryInZone.SalesOrgHierarchyId),
                    EmployeeProfile = dbContext.EmployeeProfiles.FirstOrDefault(c => c.Id == ssHistoryInZone.EmployeeId)
                }).FirstOrDefault();
        }

        public async Task<List<SSHistoryInZoneWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SSHistoryInZoneConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<SSHistoryInZoneWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from ssHistoryInZone in (await GetDbSetAsync())
                   join salesOrgHierarchy in (await GetDbContextAsync()).SalesOrgHierarchies on ssHistoryInZone.SalesOrgHierarchyId equals salesOrgHierarchy.Id into salesOrgHierarchies
                   from salesOrgHierarchy in salesOrgHierarchies.DefaultIfEmpty()
                   join employeeProfile in (await GetDbContextAsync()).EmployeeProfiles on ssHistoryInZone.EmployeeId equals employeeProfile.Id into employeeProfiles
                   from employeeProfile in employeeProfiles.DefaultIfEmpty()

                   select new SSHistoryInZoneWithNavigationProperties
                   {
                       SSHistoryInZone = ssHistoryInZone,
                       SalesOrgHierarchy = salesOrgHierarchy,
                       EmployeeProfile = employeeProfile
                   };
        }

        protected virtual IQueryable<SSHistoryInZoneWithNavigationProperties> ApplyFilter(
            IQueryable<SSHistoryInZoneWithNavigationProperties> query,
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
                    .WhereIf(effectiveDateMin.HasValue, e => e.SSHistoryInZone.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.SSHistoryInZone.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.SSHistoryInZone.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.SSHistoryInZone.EndDate <= endDateMax.Value)
                    .WhereIf(salesOrgHierarchyId != null && salesOrgHierarchyId != Guid.Empty, e => e.SalesOrgHierarchy != null && e.SalesOrgHierarchy.Id == salesOrgHierarchyId)
                    .WhereIf(employeeId != null && employeeId != Guid.Empty, e => e.EmployeeProfile != null && e.EmployeeProfile.Id == employeeId);
        }

        public async Task<List<SSHistoryInZone>> GetListAsync(
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
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SSHistoryInZoneConsts.GetDefaultSorting(false) : sorting);
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

        protected virtual IQueryable<SSHistoryInZone> ApplyFilter(
            IQueryable<SSHistoryInZone> query,
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
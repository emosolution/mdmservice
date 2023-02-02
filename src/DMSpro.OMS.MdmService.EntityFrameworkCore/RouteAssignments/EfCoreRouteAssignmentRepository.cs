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

namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public partial class EfCoreRouteAssignmentRepository : EfCoreRepository<MdmServiceDbContext, RouteAssignment, Guid>, IRouteAssignmentRepository
    {
        public EfCoreRouteAssignmentRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<RouteAssignmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(routeAssignment => new RouteAssignmentWithNavigationProperties
                {
                    RouteAssignment = routeAssignment,
                    SalesOrgHierarchy = dbContext.SalesOrgHierarchies.FirstOrDefault(c => c.Id == routeAssignment.RouteId),
                    EmployeeProfile = dbContext.EmployeeProfiles.FirstOrDefault(c => c.Id == routeAssignment.EmployeeId)
                }).FirstOrDefault();
        }

        public async Task<List<RouteAssignmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? routeId = null,
            Guid? employeeId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, routeId, employeeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RouteAssignmentConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<RouteAssignmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from routeAssignment in (await GetDbSetAsync())
                   join salesOrgHierarchy in (await GetDbContextAsync()).SalesOrgHierarchies on routeAssignment.RouteId equals salesOrgHierarchy.Id into salesOrgHierarchies
                   from salesOrgHierarchy in salesOrgHierarchies.DefaultIfEmpty()
                   join employeeProfile in (await GetDbContextAsync()).EmployeeProfiles on routeAssignment.EmployeeId equals employeeProfile.Id into employeeProfiles
                   from employeeProfile in employeeProfiles.DefaultIfEmpty()

                   select new RouteAssignmentWithNavigationProperties
                   {
                       RouteAssignment = routeAssignment,
                       SalesOrgHierarchy = salesOrgHierarchy,
                       EmployeeProfile = employeeProfile
                   };
        }

        protected virtual IQueryable<RouteAssignmentWithNavigationProperties> ApplyFilter(
            IQueryable<RouteAssignmentWithNavigationProperties> query,
            string filterText,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? routeId = null,
            Guid? employeeId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(effectiveDateMin.HasValue, e => e.RouteAssignment.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.RouteAssignment.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.RouteAssignment.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.RouteAssignment.EndDate <= endDateMax.Value)
                    .WhereIf(routeId != null && routeId != Guid.Empty, e => e.SalesOrgHierarchy != null && e.SalesOrgHierarchy.Id == routeId)
                    .WhereIf(employeeId != null && employeeId != Guid.Empty, e => e.EmployeeProfile != null && e.EmployeeProfile.Id == employeeId);
        }

        public async Task<List<RouteAssignment>> GetListAsync(
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
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RouteAssignmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? routeId = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, routeId, employeeId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<RouteAssignment> ApplyFilter(
            IQueryable<RouteAssignment> query,
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
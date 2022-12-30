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

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class EfCoreSalesOrgHierarchyRepository : EfCoreRepository<MdmServiceDbContext, SalesOrgHierarchy, Guid>, ISalesOrgHierarchyRepository
    {
        public EfCoreSalesOrgHierarchyRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<SalesOrgHierarchyWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(salesOrgHierarchy => new SalesOrgHierarchyWithNavigationProperties
                {
                    SalesOrgHierarchy = salesOrgHierarchy,
                    SalesOrgHeader = dbContext.SalesOrgHeaders.FirstOrDefault(c => c.Id == salesOrgHierarchy.SalesOrgHeaderId),
                    SalesOrgHierarchy1 = dbContext.SalesOrgHierarchies.FirstOrDefault(c => c.Id == salesOrgHierarchy.ParentId)
                }).FirstOrDefault();
        }

        public async Task<List<SalesOrgHierarchyWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            bool? isRoute = null,
            bool? isSellingZone = null,
            string hierarchyCode = null,
            bool? active = null,
            Guid? salesOrgHeaderId = null,
            Guid? parentId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, levelMin, levelMax, isRoute, isSellingZone, hierarchyCode, active, salesOrgHeaderId, parentId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SalesOrgHierarchyConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<SalesOrgHierarchyWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from salesOrgHierarchy in (await GetDbSetAsync())
                   join salesOrgHeader in (await GetDbContextAsync()).SalesOrgHeaders on salesOrgHierarchy.SalesOrgHeaderId equals salesOrgHeader.Id into salesOrgHeaders
                   from salesOrgHeader in salesOrgHeaders.DefaultIfEmpty()
                   join salesOrgHierarchy1 in (await GetDbContextAsync()).SalesOrgHierarchies on salesOrgHierarchy.ParentId equals salesOrgHierarchy1.Id into salesOrgHierarchies1
                   from salesOrgHierarchy1 in salesOrgHierarchies1.DefaultIfEmpty()

                   select new SalesOrgHierarchyWithNavigationProperties
                   {
                       SalesOrgHierarchy = salesOrgHierarchy,
                       SalesOrgHeader = salesOrgHeader,
                       SalesOrgHierarchy1 = salesOrgHierarchy1
                   };
        }

        protected virtual IQueryable<SalesOrgHierarchyWithNavigationProperties> ApplyFilter(
            IQueryable<SalesOrgHierarchyWithNavigationProperties> query,
            string filterText,
            string code = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            bool? isRoute = null,
            bool? isSellingZone = null,
            string hierarchyCode = null,
            bool? active = null,
            Guid? salesOrgHeaderId = null,
            Guid? parentId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.SalesOrgHierarchy.Code.Contains(filterText) || e.SalesOrgHierarchy.Name.Contains(filterText) || e.SalesOrgHierarchy.HierarchyCode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.SalesOrgHierarchy.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.SalesOrgHierarchy.Name.Contains(name))
                    .WhereIf(levelMin.HasValue, e => e.SalesOrgHierarchy.Level >= levelMin.Value)
                    .WhereIf(levelMax.HasValue, e => e.SalesOrgHierarchy.Level <= levelMax.Value)
                    .WhereIf(isRoute.HasValue, e => e.SalesOrgHierarchy.IsRoute == isRoute)
                    .WhereIf(isSellingZone.HasValue, e => e.SalesOrgHierarchy.IsSellingZone == isSellingZone)
                    .WhereIf(!string.IsNullOrWhiteSpace(hierarchyCode), e => e.SalesOrgHierarchy.HierarchyCode.Contains(hierarchyCode))
                    .WhereIf(active.HasValue, e => e.SalesOrgHierarchy.Active == active)
                    .WhereIf(salesOrgHeaderId != null && salesOrgHeaderId != Guid.Empty, e => e.SalesOrgHeader != null && e.SalesOrgHeader.Id == salesOrgHeaderId)
                    .WhereIf(parentId != null && parentId != Guid.Empty, e => e.SalesOrgHierarchy1 != null && e.SalesOrgHierarchy1.Id == parentId);
        }

        public async Task<List<SalesOrgHierarchy>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            bool? isRoute = null,
            bool? isSellingZone = null,
            string hierarchyCode = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, levelMin, levelMax, isRoute, isSellingZone, hierarchyCode, active);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SalesOrgHierarchyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            bool? isRoute = null,
            bool? isSellingZone = null,
            string hierarchyCode = null,
            bool? active = null,
            Guid? salesOrgHeaderId = null,
            Guid? parentId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, levelMin, levelMax, isRoute, isSellingZone, hierarchyCode, active, salesOrgHeaderId, parentId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<SalesOrgHierarchy> ApplyFilter(
            IQueryable<SalesOrgHierarchy> query,
            string filterText,
            string code = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            bool? isRoute = null,
            bool? isSellingZone = null,
            string hierarchyCode = null,
            bool? active = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText) || e.HierarchyCode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(levelMin.HasValue, e => e.Level >= levelMin.Value)
                    .WhereIf(levelMax.HasValue, e => e.Level <= levelMax.Value)
                    .WhereIf(isRoute.HasValue, e => e.IsRoute == isRoute)
                    .WhereIf(isSellingZone.HasValue, e => e.IsSellingZone == isSellingZone)
                    .WhereIf(!string.IsNullOrWhiteSpace(hierarchyCode), e => e.HierarchyCode.Contains(hierarchyCode))
                    .WhereIf(active.HasValue, e => e.Active == active);
        }
    }
}
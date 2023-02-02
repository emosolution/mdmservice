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

namespace DMSpro.OMS.MdmService.Routes
{
    public partial class EfCoreRouteRepository : EfCoreRepository<MdmServiceDbContext, Route, Guid>, IRouteRepository
    {
        public EfCoreRouteRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<RouteWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(route => new RouteWithNavigationProperties
                {
                    Route = route,
                    SystemData = dbContext.SystemDatas.FirstOrDefault(c => c.Id == route.RouteTypeId),
                    ItemGroup = dbContext.ItemGroups.FirstOrDefault(c => c.Id == route.ItemGroupId),
                    SalesOrgHierarchy = dbContext.SalesOrgHierarchies.FirstOrDefault(c => c.Id == route.SalesOrgHierarchyId)
                }).FirstOrDefault();
        }

        public async Task<List<RouteWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? checkIn = null,
            bool? checkOut = null,
            bool? gpsLock = null,
            bool? outRoute = null,
            Guid? routeTypeId = null,
            Guid? itemGroupId = null,
            Guid? salesOrgHierarchyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, checkIn, checkOut, gpsLock, outRoute, routeTypeId, itemGroupId, salesOrgHierarchyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RouteConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<RouteWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from route in (await GetDbSetAsync())
                   join systemData in (await GetDbContextAsync()).SystemDatas on route.RouteTypeId equals systemData.Id into systemDatas
                   from systemData in systemDatas.DefaultIfEmpty()
                   join itemGroup in (await GetDbContextAsync()).ItemGroups on route.ItemGroupId equals itemGroup.Id into itemGroups
                   from itemGroup in itemGroups.DefaultIfEmpty()
                   join salesOrgHierarchy in (await GetDbContextAsync()).SalesOrgHierarchies on route.SalesOrgHierarchyId equals salesOrgHierarchy.Id into salesOrgHierarchies
                   from salesOrgHierarchy in salesOrgHierarchies.DefaultIfEmpty()

                   select new RouteWithNavigationProperties
                   {
                       Route = route,
                       SystemData = systemData,
                       ItemGroup = itemGroup,
                       SalesOrgHierarchy = salesOrgHierarchy
                   };
        }

        protected virtual IQueryable<RouteWithNavigationProperties> ApplyFilter(
            IQueryable<RouteWithNavigationProperties> query,
            string filterText,
            bool? checkIn = null,
            bool? checkOut = null,
            bool? gpsLock = null,
            bool? outRoute = null,
            Guid? routeTypeId = null,
            Guid? itemGroupId = null,
            Guid? salesOrgHierarchyId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(checkIn.HasValue, e => e.Route.CheckIn == checkIn)
                    .WhereIf(checkOut.HasValue, e => e.Route.CheckOut == checkOut)
                    .WhereIf(gpsLock.HasValue, e => e.Route.GPSLock == gpsLock)
                    .WhereIf(outRoute.HasValue, e => e.Route.OutRoute == outRoute)
                    .WhereIf(routeTypeId != null && routeTypeId != Guid.Empty, e => e.SystemData != null && e.SystemData.Id == routeTypeId)
                    .WhereIf(itemGroupId != null && itemGroupId != Guid.Empty, e => e.ItemGroup != null && e.ItemGroup.Id == itemGroupId)
                    .WhereIf(salesOrgHierarchyId != null && salesOrgHierarchyId != Guid.Empty, e => e.SalesOrgHierarchy != null && e.SalesOrgHierarchy.Id == salesOrgHierarchyId);
        }

        public async Task<List<Route>> GetListAsync(
            string filterText = null,
            bool? checkIn = null,
            bool? checkOut = null,
            bool? gpsLock = null,
            bool? outRoute = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, checkIn, checkOut, gpsLock, outRoute);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RouteConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            bool? checkIn = null,
            bool? checkOut = null,
            bool? gpsLock = null,
            bool? outRoute = null,
            Guid? routeTypeId = null,
            Guid? itemGroupId = null,
            Guid? salesOrgHierarchyId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, checkIn, checkOut, gpsLock, outRoute, routeTypeId, itemGroupId, salesOrgHierarchyId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Route> ApplyFilter(
            IQueryable<Route> query,
            string filterText,
            bool? checkIn = null,
            bool? checkOut = null,
            bool? gpsLock = null,
            bool? outRoute = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(checkIn.HasValue, e => e.CheckIn == checkIn)
                    .WhereIf(checkOut.HasValue, e => e.CheckOut == checkOut)
                    .WhereIf(gpsLock.HasValue, e => e.GPSLock == gpsLock)
                    .WhereIf(outRoute.HasValue, e => e.OutRoute == outRoute);
        }
    }
}
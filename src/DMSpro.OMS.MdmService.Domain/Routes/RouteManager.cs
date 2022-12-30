using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.Routes
{
    public class RouteManager : DomainService
    {
        private readonly IRouteRepository _routeRepository;

        public RouteManager(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<Route> CreateAsync(
        Guid routeTypeId, Guid itemGroupId, Guid salesOrgHierarchyId, bool checkIn, bool checkOut, bool gpsLock, bool outRoute)
        {
            Check.NotNull(routeTypeId, nameof(routeTypeId));
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));

            var route = new Route(
             GuidGenerator.Create(),
             routeTypeId, itemGroupId, salesOrgHierarchyId, checkIn, checkOut, gpsLock, outRoute
             );

            return await _routeRepository.InsertAsync(route);
        }

        public async Task<Route> UpdateAsync(
            Guid id,
            Guid routeTypeId, Guid itemGroupId, Guid salesOrgHierarchyId, bool checkIn, bool checkOut, bool gpsLock, bool outRoute, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(routeTypeId, nameof(routeTypeId));
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));

            var queryable = await _routeRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var route = await AsyncExecuter.FirstOrDefaultAsync(query);

            route.RouteTypeId = routeTypeId;
            route.ItemGroupId = itemGroupId;
            route.SalesOrgHierarchyId = salesOrgHierarchyId;
            route.CheckIn = checkIn;
            route.CheckOut = checkOut;
            route.GPSLock = gpsLock;
            route.OutRoute = outRoute;

            route.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _routeRepository.UpdateAsync(route);
        }

    }
}
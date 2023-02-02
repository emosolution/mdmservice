using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.Routes
{
    public partial interface IRouteRepository : IRepository<Route, Guid>
    {
        Task<RouteWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<RouteWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<Route>> GetListAsync(
                    string filterText = null,
                    bool? checkIn = null,
                    bool? checkOut = null,
                    bool? gpsLock = null,
                    bool? outRoute = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            bool? checkIn = null,
            bool? checkOut = null,
            bool? gpsLock = null,
            bool? outRoute = null,
            Guid? routeTypeId = null,
            Guid? itemGroupId = null,
            Guid? salesOrgHierarchyId = null,
            CancellationToken cancellationToken = default);
    }
}
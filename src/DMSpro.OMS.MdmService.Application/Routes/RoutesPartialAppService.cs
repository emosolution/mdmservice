using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.ItemGroups;

namespace DMSpro.OMS.MdmService.Routes
{
    [Authorize(MdmServicePermissions.Routes.Default)]
    public partial class RoutesAppService : PartialAppService<Route, RouteDto, IRouteRepository>,
        IRoutesAppService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IDistributedCache<RouteExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly RouteManager _routeManager;

        private readonly ISystemDataRepository _systemDataRepository;
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly IItemGroupRepository _itemGroupRepository;

        public RoutesAppService(ICurrentTenant currentTenant,
            IRouteRepository repository,
            RouteManager routeManager,
            IConfiguration settingProvider,
            ISystemDataRepository systemDataRepository,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            IItemGroupRepository itemGroupRepository,
            IDistributedCache<RouteExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _routeRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _routeManager = routeManager;

            _systemDataRepository = systemDataRepository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _itemGroupRepository = itemGroupRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IRouteRepository", _routeRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISystemDataRepository", _systemDataRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupRepository", _itemGroupRepository));
        }
    }
}
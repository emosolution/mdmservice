using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.Routes;

namespace DMSpro.OMS.MdmService.Routes
{
    public class RoutesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IRouteRepository _routeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SystemDatasDataSeedContributor _systemDatasDataSeedContributor;

        private readonly ItemGroupsDataSeedContributor _itemGroupsDataSeedContributor;

        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        public RoutesDataSeedContributor(IRouteRepository routeRepository, IUnitOfWorkManager unitOfWorkManager, SystemDatasDataSeedContributor systemDatasDataSeedContributor, ItemGroupsDataSeedContributor itemGroupsDataSeedContributor, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor)
        {
            _routeRepository = routeRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _systemDatasDataSeedContributor = systemDatasDataSeedContributor; _itemGroupsDataSeedContributor = itemGroupsDataSeedContributor; _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _systemDatasDataSeedContributor.SeedAsync(context);
            await _itemGroupsDataSeedContributor.SeedAsync(context);
            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);

            await _routeRepository.InsertAsync(new Route
            (
                id: Guid.Parse("79d7c3ca-a497-49a8-8f14-3ee644bebb61"),
                checkIn: true,
                checkOut: true,
                gpsLock: true,
                outRoute: true,
                routeTypeId: Guid.Parse("85a4dc75-61e7-42e3-87b7-da8004347d49"),
                itemGroupId: Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5")
            ));

            await _routeRepository.InsertAsync(new Route
            (
                id: Guid.Parse("2f432da4-633a-4872-a1cf-1b23f6f923af"),
                checkIn: true,
                checkOut: true,
                gpsLock: true,
                outRoute: true,
                routeTypeId: Guid.Parse("85a4dc75-61e7-42e3-87b7-da8004347d49"),
                itemGroupId: Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
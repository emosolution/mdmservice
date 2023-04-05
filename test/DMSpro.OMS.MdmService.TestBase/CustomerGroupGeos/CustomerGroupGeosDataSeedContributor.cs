using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerGroupGeos;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeosDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerGroupGeoRepository _customerGroupGeoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor;

        public CustomerGroupGeosDataSeedContributor(ICustomerGroupGeoRepository customerGroupGeoRepository, IUnitOfWorkManager unitOfWorkManager, CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor, GeoMastersDataSeedContributor geoMastersDataSeedContributor, GeoMastersDataSeedContributor geoMastersDataSeedContributor, GeoMastersDataSeedContributor geoMastersDataSeedContributor, GeoMastersDataSeedContributor geoMastersDataSeedContributor, GeoMastersDataSeedContributor geoMastersDataSeedContributor)
        {
            _customerGroupGeoRepository = customerGroupGeoRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customerGroupsDataSeedContributor = customerGroupsDataSeedContributor; _geoMastersDataSeedContributor = geoMastersDataSeedContributor; _geoMastersDataSeedContributor = geoMastersDataSeedContributor; _geoMastersDataSeedContributor = geoMastersDataSeedContributor; _geoMastersDataSeedContributor = geoMastersDataSeedContributor; _geoMastersDataSeedContributor = geoMastersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerGroupsDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor.SeedAsync(context);

            await _customerGroupGeoRepository.InsertAsync(new CustomerGroupGeo
            (
                id: Guid.Parse("421897c5-aa57-451f-aab6-deab196cf440"),
                description: "429320152b174aa1a47f",
                active: true,
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                geoMaster0Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                geoMaster1Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                geoMaster2Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                geoMaster3Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                geoMaster4Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367")
            ));

            await _customerGroupGeoRepository.InsertAsync(new CustomerGroupGeo
            (
                id: Guid.Parse("d7f0d2d0-ba61-487d-b3d4-6ef61013e063"),
                description: "c25108633e834483874c",
                active: true,
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                geoMaster0Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                geoMaster1Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                geoMaster2Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                geoMaster3Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                geoMaster4Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
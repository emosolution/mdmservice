using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeosDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerGroupGeoRepository _customerGroupGeoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor0;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor1;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor2;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor3;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor4;

        public CustomerGroupGeosDataSeedContributor(ICustomerGroupGeoRepository customerGroupGeoRepository, IUnitOfWorkManager unitOfWorkManager, 
            CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor,
            GeoMastersDataSeedContributor geoMastersDataSeedContributor0, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor1, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor2,
            GeoMastersDataSeedContributor geoMastersDataSeedContributor3,
            GeoMastersDataSeedContributor geoMastersDataSeedContributor4)
        {
            _customerGroupGeoRepository = customerGroupGeoRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customerGroupsDataSeedContributor = customerGroupsDataSeedContributor; 
            _geoMastersDataSeedContributor0 = geoMastersDataSeedContributor0; 
            _geoMastersDataSeedContributor1 = geoMastersDataSeedContributor1; 
            _geoMastersDataSeedContributor2 = geoMastersDataSeedContributor2; 
            _geoMastersDataSeedContributor3 = geoMastersDataSeedContributor3; 
            _geoMastersDataSeedContributor4 = geoMastersDataSeedContributor4;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerGroupsDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor0.SeedAsync(context);
            await _geoMastersDataSeedContributor1.SeedAsync(context);
            await _geoMastersDataSeedContributor2.SeedAsync(context);
            await _geoMastersDataSeedContributor3.SeedAsync(context);
            await _geoMastersDataSeedContributor4.SeedAsync(context);

            await _customerGroupGeoRepository.InsertAsync(new CustomerGroupGeo
            (
                id: Guid.Parse("c9aaecb7-1e9f-4aeb-a020-dee128c79ac8"),
                description: "b5811d8743664340be72",
                active: true,
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                geoMaster0Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null
            ));

            await _customerGroupGeoRepository.InsertAsync(new CustomerGroupGeo
            (
                id: Guid.Parse("93ef0840-8066-441e-ac7e-f10f0f37b8e5"),
                description: "4ffd593097d640cdb7e8",
                active: true,
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                geoMaster0Id: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public class CustomerGroupByGeosDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerGroupByGeoRepository _customerGroupByGeoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor0;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor1;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor2;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor3;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor4;

        public CustomerGroupByGeosDataSeedContributor(ICustomerGroupByGeoRepository customerGroupByGeoRepository, IUnitOfWorkManager unitOfWorkManager, 
            CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor0, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor1, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor2, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor3, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor4)
        {
            _customerGroupByGeoRepository = customerGroupByGeoRepository;
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

            await _customerGroupByGeoRepository.InsertAsync(new CustomerGroupByGeo
            (
                id: Guid.Parse("d80a2a0d-fbc9-43f0-8b13-f0ca571774ef"),
                active: true,
                effectiveDate: new DateTime(2014, 2, 19),
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null
            ));

            await _customerGroupByGeoRepository.InsertAsync(new CustomerGroupByGeo
            (
                id: Guid.Parse("cb2f83d4-2dbd-4947-b02b-bf67d993b48e"),
                active: true,
                effectiveDate: new DateTime(2001, 10, 3),
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                geoMaster0Id: null,
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
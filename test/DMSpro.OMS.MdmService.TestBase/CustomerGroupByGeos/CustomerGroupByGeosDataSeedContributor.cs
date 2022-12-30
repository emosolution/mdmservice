using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerGroupByGeos;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public class CustomerGroupByGeosDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerGroupByGeoRepository _customerGroupByGeoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor;

        public CustomerGroupByGeosDataSeedContributor(ICustomerGroupByGeoRepository customerGroupByGeoRepository, IUnitOfWorkManager unitOfWorkManager, CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor, GeoMastersDataSeedContributor geoMastersDataSeedContributor)
        {
            _customerGroupByGeoRepository = customerGroupByGeoRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customerGroupsDataSeedContributor = customerGroupsDataSeedContributor; _geoMastersDataSeedContributor = geoMastersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerGroupsDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor.SeedAsync(context);

            await _customerGroupByGeoRepository.InsertAsync(new CustomerGroupByGeo
            (
                id: Guid.Parse("d647c0d8-11d5-45ac-a94a-26b92b3db71a"),
                active: true,
                effectiveDate: new DateTime(2005, 9, 24),
                customerGroupId: Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                geoMasterId: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367")
            ));

            await _customerGroupByGeoRepository.InsertAsync(new CustomerGroupByGeo
            (
                id: Guid.Parse("eef99443-cea3-4efc-851c-935a9e06531f"),
                active: true,
                effectiveDate: new DateTime(2006, 7, 6),
                customerGroupId: Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                geoMasterId: Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
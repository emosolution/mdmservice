using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerInZones;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public class CustomerInZonesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerInZoneRepository _customerInZoneRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        public CustomerInZonesDataSeedContributor(ICustomerInZoneRepository customerInZoneRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, CustomersDataSeedContributor customersDataSeedContributor)
        {
            _customerInZoneRepository = customerInZoneRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor; _customersDataSeedContributor = customersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);
            await _customersDataSeedContributor.SeedAsync(context);

            await _customerInZoneRepository.InsertAsync(new CustomerInZone
            (
                id: Guid.Parse("55164995-b1b0-40b1-8070-cbc6b7b2a338"),
                effectiveDate: new DateTime(2009, 5, 27),
                endDate: new DateTime(2012, 4, 7),
                salesOrgHierarchyId: Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                customerId: Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            ));

            await _customerInZoneRepository.InsertAsync(new CustomerInZone
            (
                id: Guid.Parse("1bc3b736-6826-46c8-a7ce-4c374f3b6b77"),
                effectiveDate: new DateTime(2020, 3, 23),
                endDate: new DateTime(2000, 1, 19),
                salesOrgHierarchyId: Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                customerId: Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
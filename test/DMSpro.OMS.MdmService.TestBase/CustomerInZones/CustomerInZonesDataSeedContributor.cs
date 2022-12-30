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

        public CustomerInZonesDataSeedContributor(ICustomerInZoneRepository customerInZoneRepository, IUnitOfWorkManager unitOfWorkManager,  
            CustomersDataSeedContributor customersDataSeedContributor,
            SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor)
        {
            _customerInZoneRepository = customerInZoneRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor;
            _customersDataSeedContributor = customersDataSeedContributor;
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
                id: Guid.Parse("ece3682b-9037-4262-89fc-3193b52afd39"),
                effectiveDate: new DateTime(2017, 7, 26),
                endDate: new DateTime(2007, 10, 10),
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            ));

            await _customerInZoneRepository.InsertAsync(new CustomerInZone
            (
                id: Guid.Parse("c5053680-b579-49de-8cbe-a30adc7fed0d"),
                effectiveDate: new DateTime(2016, 10, 24),
                endDate: new DateTime(2003, 11, 25),
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
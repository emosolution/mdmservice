using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.SSHistoryInZones;

namespace DMSpro.OMS.MdmService.SSHistoryInZones
{
    public class SSHistoryInZonesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISSHistoryInZoneRepository _sSHistoryInZoneRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        private readonly EmployeeProfilesDataSeedContributor _employeeProfilesDataSeedContributor;

        public SSHistoryInZonesDataSeedContributor(ISSHistoryInZoneRepository sSHistoryInZoneRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, EmployeeProfilesDataSeedContributor employeeProfilesDataSeedContributor)
        {
            _sSHistoryInZoneRepository = sSHistoryInZoneRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor; _employeeProfilesDataSeedContributor = employeeProfilesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);
            await _employeeProfilesDataSeedContributor.SeedAsync(context);

            await _sSHistoryInZoneRepository.InsertAsync(new SSHistoryInZone
            (
                id: Guid.Parse("6628ad79-717b-492a-9be6-13711bdfdafa"),
                effectiveDate: new DateTime(2018, 11, 24),
                endDate: new DateTime(2015, 3, 4),
                salesOrgHierarchyId: Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                employeeId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _sSHistoryInZoneRepository.InsertAsync(new SSHistoryInZone
            (
                id: Guid.Parse("5075557b-0bc5-4f04-865a-37b70e258503"),
                effectiveDate: new DateTime(2011, 6, 25),
                endDate: new DateTime(2000, 11, 18),
                salesOrgHierarchyId: Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                employeeId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
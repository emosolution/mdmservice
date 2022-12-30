using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.EmployeeInZones;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public class EmployeeInZonesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IEmployeeInZoneRepository _employeeInZoneRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        private readonly EmployeeProfilesDataSeedContributor _employeeProfilesDataSeedContributor;

        public EmployeeInZonesDataSeedContributor(IEmployeeInZoneRepository employeeInZoneRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, EmployeeProfilesDataSeedContributor employeeProfilesDataSeedContributor)
        {
            _employeeInZoneRepository = employeeInZoneRepository;
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

            await _employeeInZoneRepository.InsertAsync(new EmployeeInZone
            (
                id: Guid.Parse("be95496e-4382-4f20-ab36-cc18be96ebe8"),
                effectiveDate: new DateTime(2001, 9, 6),
                endDate: Guid.Parse("6c47627f-7496-401a-90db-59b89dd7019e"),
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                employeeId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _employeeInZoneRepository.InsertAsync(new EmployeeInZone
            (
                id: Guid.Parse("7c756745-3011-407e-b0c8-bcc8093cb4fc"),
                effectiveDate: new DateTime(2003, 8, 2),
                endDate: Guid.Parse("be0d0193-1f6e-4489-adbd-76dcb8d14f93"),
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                employeeId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
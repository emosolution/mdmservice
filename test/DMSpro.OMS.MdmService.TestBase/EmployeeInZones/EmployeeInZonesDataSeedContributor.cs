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
                id: Guid.Parse("5ad99832-9ebd-4aa5-87bd-230e7e14caf4"),
                effectiveDate: new DateTime(2003, 3, 14),
                endDate: new DateTime(2003, 8, 6),
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                employeeId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _employeeInZoneRepository.InsertAsync(new EmployeeInZone
            (
                id: Guid.Parse("0c446f99-9591-42d1-ae7a-2016ab9db854"),
                effectiveDate: new DateTime(2011, 1, 26),
                endDate: new DateTime(2001, 10, 19),
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                employeeId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
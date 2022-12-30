using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISalesOrgEmpAssignmentRepository _salesOrgEmpAssignmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        private readonly EmployeeProfilesDataSeedContributor _employeeProfilesDataSeedContributor;

        public SalesOrgEmpAssignmentsDataSeedContributor(ISalesOrgEmpAssignmentRepository salesOrgEmpAssignmentRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, EmployeeProfilesDataSeedContributor employeeProfilesDataSeedContributor)
        {
            _salesOrgEmpAssignmentRepository = salesOrgEmpAssignmentRepository;
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

            await _salesOrgEmpAssignmentRepository.InsertAsync(new SalesOrgEmpAssignment
            (
                id: Guid.Parse("96d9bae3-9a5b-483b-a9bc-0e164419d17a"),
                isBase: true,
                effectiveDate: new DateTime(2021, 1, 23),
                endDate: new DateTime(2011, 9, 20),
                salesOrgHierarchyId: Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _salesOrgEmpAssignmentRepository.InsertAsync(new SalesOrgEmpAssignment
            (
                id: Guid.Parse("11051961-14d4-454e-965d-d2502af59e8b"),
                isBase: true,
                effectiveDate: new DateTime(2021, 6, 23),
                endDate: new DateTime(2006, 4, 19),
                salesOrgHierarchyId: Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
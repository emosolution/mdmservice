using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.RouteAssignments;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public class RouteAssignmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IRouteAssignmentRepository _routeAssignmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        private readonly EmployeeProfilesDataSeedContributor _employeeProfilesDataSeedContributor;

        public RouteAssignmentsDataSeedContributor(IRouteAssignmentRepository routeAssignmentRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, EmployeeProfilesDataSeedContributor employeeProfilesDataSeedContributor)
        {
            _routeAssignmentRepository = routeAssignmentRepository;
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

            await _routeAssignmentRepository.InsertAsync(new RouteAssignment
            (
                id: Guid.Parse("794f8ec0-a524-4a91-bd38-595b2d27d7dd"),
                effectiveDate: new DateTime(2001, 10, 3),
                endDate: new DateTime(2002, 5, 4),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                employeeId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _routeAssignmentRepository.InsertAsync(new RouteAssignment
            (
                id: Guid.Parse("837dc18d-5124-4243-9494-c355fefaa929"),
                effectiveDate: new DateTime(2007, 6, 19),
                endDate: new DateTime(2000, 8, 13),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                employeeId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.EmployeeProfiles;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
	[Authorize(MdmServicePermissions.SalesOrgEmpAssignments.Default)]
	public partial class SalesOrgEmpAssignmentsAppService : PartialAppService<SalesOrgEmpAssignment, SalesOrgEmpAssignmentWithDetailsDto, ISalesOrgEmpAssignmentRepository>,
		ISalesOrgEmpAssignmentsAppService
	{
		private readonly ISalesOrgEmpAssignmentRepository _salesOrgEmpAssignmentRepository;

		private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
		private readonly IEmployeeProfileRepository _employeeProfileRepository;

		public SalesOrgEmpAssignmentsAppService(ICurrentTenant currentTenant,
			ISalesOrgEmpAssignmentRepository repository,
			IConfiguration settingProvider,
			ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
			IEmployeeProfileRepository employeeProfileRepository)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.SalesOrgEmpAssignments.Default)
		{
			_salesOrgEmpAssignmentRepository = repository;

			_salesOrgHierarchyRepository= salesOrgHierarchyRepository;
			_employeeProfileRepository = employeeProfileRepository;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgEmpAssignmentRepository", _salesOrgEmpAssignmentRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                    new KeyValuePair<string, object>("IEmployeeProfileRepository", _employeeProfileRepository));
        }
    }
}
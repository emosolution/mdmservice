using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System.Runtime.CompilerServices;
using DMSpro.OMS.MdmService.EmployeeProfiles;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
	[Authorize(MdmServicePermissions.RouteAssignments.Default)]
	public partial class RouteAssignmentsAppService : PartialAppService<RouteAssignment, RouteAssignmentDto, IRouteAssignmentRepository>,
		IRouteAssignmentsAppService
	{
		private readonly IRouteAssignmentRepository _routeAssignmentRepository;
		private readonly IDistributedCache<RouteAssignmentExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly RouteAssignmentManager _routeAssignmentManager;

		private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
		private readonly IEmployeeProfileRepository _employeeProfileRepository;

		public RouteAssignmentsAppService(ICurrentTenant currentTenant,
			IRouteAssignmentRepository repository,
			RouteAssignmentManager routeAssignmentManager,
			IConfiguration settingProvider,
			ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
			IEmployeeProfileRepository employeeProfileRepository,
			IDistributedCache<RouteAssignmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_routeAssignmentRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_routeAssignmentManager = routeAssignmentManager;
			
			_salesOrgHierarchyRepository = salesOrgHierarchyRepository;
			_employeeProfileRepository = employeeProfileRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IRouteAssignmentRepository", _routeAssignmentRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IEmployeeProfileRepository", _employeeProfileRepository));
        }
    }
}
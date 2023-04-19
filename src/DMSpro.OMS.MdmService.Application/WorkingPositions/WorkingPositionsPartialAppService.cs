using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.EmployeeProfiles;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
	[Authorize(MdmServicePermissions.WorkingPositions.Default)]
	public partial class WorkingPositionsAppService : PartialAppService<WorkingPosition, WorkingPositionDto, IWorkingPositionRepository>,
		IWorkingPositionsAppService
	{
		private readonly IWorkingPositionRepository _workingPositionRepository;
        private readonly IEmployeeProfileRepository _employeeProfileRepository;

        public WorkingPositionsAppService(ICurrentTenant currentTenant,
			IWorkingPositionRepository repository,
			IConfiguration settingProvider,
			IEmployeeProfileRepository employeeProfileRepository)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.WorkingPositions.Default)
		{
			_workingPositionRepository = repository;
            _employeeProfileRepository = employeeProfileRepository;
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IWorkingPositionRepository", _workingPositionRepository));
		}
    }
}
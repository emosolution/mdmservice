using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
	[Authorize(MdmServicePermissions.WorkingPositions.Default)]
	public partial class WorkingPositionsAppService : PartialAppService<WorkingPosition, WorkingPositionDto, IWorkingPositionRepository>,
		IWorkingPositionsAppService
	{
		private readonly IWorkingPositionRepository _workingPositionRepository;
		private readonly IDistributedCache<WorkingPositionExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly WorkingPositionManager _workingPositionManager;

		public WorkingPositionsAppService(ICurrentTenant currentTenant,
			IWorkingPositionRepository repository,
			WorkingPositionManager workingPositionManager,
			IConfiguration settingProvider,
			IDistributedCache<WorkingPositionExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_workingPositionRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_workingPositionManager = workingPositionManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IWorkingPositionRepository", _workingPositionRepository));
		}
    }
}
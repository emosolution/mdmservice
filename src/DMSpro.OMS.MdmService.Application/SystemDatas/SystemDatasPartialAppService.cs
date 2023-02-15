using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.SystemDatas
{
	[Authorize(MdmServicePermissions.SystemData.Default)]
	public partial class SystemDatasAppService : PartialAppService<SystemData, SystemDataDto, ISystemDataRepository>,
		ISystemDatasAppService
	{
		private readonly ISystemDataRepository _systemDataRepository;
		private readonly IDistributedCache<SystemDataExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly SystemDataManager _systemDataManager;

		public SystemDatasAppService(ICurrentTenant currentTenant,
			ISystemDataRepository repository,
			SystemDataManager systemDataManager,
			IConfiguration settingProvider,
			IDistributedCache<SystemDataExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_systemDataRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_systemDataManager = systemDataManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISystemDataRepository", _systemDataRepository));
		}
    }
}
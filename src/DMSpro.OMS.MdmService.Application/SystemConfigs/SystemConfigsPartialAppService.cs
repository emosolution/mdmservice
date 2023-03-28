using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
	[Authorize(MdmServicePermissions.SystemConfig.Default)]
	public partial class SystemConfigsAppService : PartialAppService<SystemConfig, SystemConfigDto, ISystemConfigRepository>,
		ISystemConfigsAppService
	{
		private readonly ISystemConfigRepository _systemConfigRepository;
		private readonly IDistributedCache<SystemConfigExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly SystemConfigManager _systemConfigManager;

		public SystemConfigsAppService(ICurrentTenant currentTenant,
			ISystemConfigRepository repository,
			SystemConfigManager systemConfigManager,
			IConfiguration settingProvider,
			IDistributedCache<SystemConfigExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.SystemConfig.Default)
		{
			_systemConfigRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_systemConfigManager = systemConfigManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISystemConfigRepository", _systemConfigRepository));
		}
    }
}
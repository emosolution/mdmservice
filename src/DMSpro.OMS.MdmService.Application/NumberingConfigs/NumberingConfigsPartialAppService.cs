using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
	[Authorize(MdmServicePermissions.NumberingConfigs.Default)]
	public partial class NumberingConfigsAppService : PartialAppService<NumberingConfig, NumberingConfigWithDetailsDto, INumberingConfigRepository>,
		INumberingConfigsAppService
	{
		private readonly INumberingConfigRepository _numberingConfigRepository;
		private readonly IDistributedCache<NumberingConfigExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly NumberingConfigManager _numberingConfigManager;

		private readonly ISystemDataRepository _systemDataRepository;

		public NumberingConfigsAppService(ICurrentTenant currentTenant,
			INumberingConfigRepository repository,
			NumberingConfigManager numberingConfigManager,
			IConfiguration settingProvider,
			ISystemDataRepository systemDataRepository,
			IDistributedCache<NumberingConfigExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_numberingConfigRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_numberingConfigManager = numberingConfigManager;
			
			_systemDataRepository= systemDataRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("INumberingConfigRepository", _numberingConfigRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISystemDataRepository", _systemDataRepository));
        }
    }
}
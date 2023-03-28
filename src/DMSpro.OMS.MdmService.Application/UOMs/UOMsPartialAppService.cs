using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.UOMs
{
	[Authorize(MdmServicePermissions.UOMs.Default)]
	public partial class UOMsAppService : PartialAppService<UOM, UOMDto, IUOMRepository>,
		IUOMsAppService
	{
		private readonly IUOMRepository _uOMRepository;
		private readonly IDistributedCache<UOMExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly UOMManager _uOMManager;

		public UOMsAppService(ICurrentTenant currentTenant,
			IUOMRepository repository,
			UOMManager uOMManager,
			IConfiguration settingProvider,
			IDistributedCache<UOMExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.UOMs.Default)
		{
			_uOMRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_uOMManager = uOMManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMRepository", _uOMRepository));
		}
    }
}
using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.Streets
{
	[Authorize(MdmServicePermissions.Streets.Default)]
	public partial class StreetsAppService : PartialAppService<Street, StreetDto, IStreetRepository>,
		IStreetsAppService
	{
		private readonly IStreetRepository _streetRepository;
		private readonly IDistributedCache<StreetExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly StreetManager _streetManager;

		public StreetsAppService(ICurrentTenant currentTenant,
			IStreetRepository repository,
			StreetManager streetManager,
			IConfiguration settingProvider,
			IDistributedCache<StreetExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.Streets.Default)
		{
			_streetRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_streetManager = streetManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IStreetRepository", _streetRepository));
		}
    }
}
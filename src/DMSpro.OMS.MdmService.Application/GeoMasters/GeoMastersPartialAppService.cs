using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.GeoMasters
{
	[Authorize(MdmServicePermissions.GeoMasters.Default)]
	public partial class GeoMastersAppService : PartialAppService<GeoMaster, GeoMasterDto, IGeoMasterRepository>,
		IGeoMastersAppService
	{
		private readonly IGeoMasterRepository _geoMasterRepository;
		private readonly IDistributedCache<GeoMasterExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly GeoMasterManager _geoMasterManager;

		public GeoMastersAppService(ICurrentTenant currentTenant,
			IGeoMasterRepository repository,
			GeoMasterManager geoMasterManager,
			IConfiguration settingProvider,
			IDistributedCache<GeoMasterExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_geoMasterRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_geoMasterManager = geoMasterManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IGeoMasterRepository", _geoMasterRepository));
		}
    }
}
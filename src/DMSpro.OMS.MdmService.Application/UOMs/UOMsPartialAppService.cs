using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMGroupDetails;

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
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;

        public UOMsAppService(ICurrentTenant currentTenant,
			IUOMRepository repository,
			UOMManager uOMManager,
			IUOMGroupDetailRepository uOMGroupDetailRepository,
			IConfiguration settingProvider,
			IDistributedCache<UOMExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_uOMRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_uOMManager = uOMManager;
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMRepository", _uOMRepository));
		}
    }
}
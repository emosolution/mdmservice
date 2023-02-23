using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.UOMGroups;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
	[Authorize(MdmServicePermissions.UOMGroupDetails.Default)]
	public partial class UOMGroupDetailsAppService : PartialAppService<UOMGroupDetail, UOMGroupDetailWithDetailsDto, IUOMGroupDetailRepository>,
		IUOMGroupDetailsAppService
	{
		private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;
		private readonly IDistributedCache<UOMGroupDetailExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly UOMGroupDetailManager _uOMGroupDetailManager;

		private readonly IUOMRepository _uOMRepository;
		private readonly IUOMGroupRepository _uOMGroupRepository;

		public UOMGroupDetailsAppService(ICurrentTenant currentTenant,
			IUOMGroupDetailRepository repository,
			UOMGroupDetailManager uOMGroupDetailManager,
			IConfiguration settingProvider,
			IUOMRepository uOMRepository,
			IUOMGroupRepository uOMGroupRepository,
			IDistributedCache<UOMGroupDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_uOMGroupDetailRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_uOMGroupDetailManager = uOMGroupDetailManager;
			
			_uOMRepository = uOMRepository;
			_uOMGroupRepository= uOMGroupRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMGroupDetailRepository", _uOMGroupDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMRepository", _uOMRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMGroupRepository", _uOMGroupRepository));
        }
    }
}
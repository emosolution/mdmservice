using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.UOMGroups
{
	[Authorize(MdmServicePermissions.UOMGroups.Default)]
	public partial class UOMGroupsAppService : PartialAppService<UOMGroup, UOMGroupWithDetailsDto, IUOMGroupRepository>,
		IUOMGroupsAppService
	{
		private readonly IUOMGroupRepository _uOMGroupRepository;
		private readonly IDistributedCache<UOMGroupExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly UOMGroupManager _uOMGroupManager;

		public UOMGroupsAppService(ICurrentTenant currentTenant,
			IUOMGroupRepository repository,
			UOMGroupManager uOMGroupManager,
			IConfiguration settingProvider,
			IDistributedCache<UOMGroupExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_uOMGroupRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_uOMGroupManager = uOMGroupManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMGroupRepository", _uOMGroupRepository));
		}
    }
}
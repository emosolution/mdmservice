using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.Items;

namespace DMSpro.OMS.MdmService.UOMGroups
{
	[Authorize(MdmServicePermissions.UOMGroups.Default)]
	public partial class UOMGroupsAppService : PartialAppService<UOMGroup, UOMGroupWithDetailsDto, IUOMGroupRepository>,
		IUOMGroupsAppService
	{
		private readonly IUOMGroupRepository _uOMGroupRepository;
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IDistributedCache<UOMGroupExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly UOMGroupManager _uOMGroupManager;

		public UOMGroupsAppService(ICurrentTenant currentTenant,
			IUOMGroupRepository repository,
			IUOMGroupDetailRepository uOMGroupDetailRepository,
			IItemRepository itemRepository,
			UOMGroupManager uOMGroupManager,
			IConfiguration settingProvider,
			IDistributedCache<UOMGroupExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.UOMGroups.Default)
		{
			_uOMGroupRepository = repository;
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
            _itemRepository = itemRepository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
			_uOMGroupManager = uOMGroupManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMGroupRepository", _uOMGroupRepository));
		}
    }
}
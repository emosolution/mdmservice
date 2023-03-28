using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.ItemGroups
{
	[Authorize(MdmServicePermissions.ItemGroups.Default)]
	public partial class ItemGroupsAppService : PartialAppService<ItemGroup, ItemGroupDto, IItemGroupRepository>,
		IItemGroupsAppService
	{
		private readonly IItemGroupRepository _itemGroupRepository;
		private readonly IDistributedCache<ItemGroupExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly ItemGroupManager _itemGroupManager;

		public ItemGroupsAppService(ICurrentTenant currentTenant,
			IItemGroupRepository repository,
			ItemGroupManager itemGroupManager,
			IConfiguration settingProvider,
			IDistributedCache<ItemGroupExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.ItemGroups.Default)
		{
			_itemGroupRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_itemGroupManager = itemGroupManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupRepository", _itemGroupRepository));
		}
    }
}
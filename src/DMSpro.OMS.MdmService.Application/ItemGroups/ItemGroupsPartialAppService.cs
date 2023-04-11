using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using DMSpro.OMS.MdmService.ItemGroupLists;

namespace DMSpro.OMS.MdmService.ItemGroups
{
	[Authorize(MdmServicePermissions.ItemGroups.Default)]
	public partial class ItemGroupsAppService : PartialAppService<ItemGroup, ItemGroupDto, IItemGroupRepository>,
		IItemGroupsAppService
	{
		private readonly IItemGroupRepository _itemGroupRepository;
		private readonly ItemGroupManager _itemGroupManager;
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;
        private readonly IItemGroupListRepository _itemGroupListRepository;

        public ItemGroupsAppService(ICurrentTenant currentTenant,
			IItemGroupRepository repository,
			ItemGroupManager itemGroupManager,
			IItemGroupAttributeRepository itemGroupAttributeRepository,
			IItemGroupListRepository itemGroupListRepository,
			IConfiguration settingProvider)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.ItemGroups.Default)
		{
			_itemGroupRepository = repository;
			_itemGroupManager = itemGroupManager;
            _itemGroupAttributeRepository = itemGroupAttributeRepository;
            _itemGroupListRepository = itemGroupListRepository;
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupRepository", _itemGroupRepository));
		}
    }
}
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.ItemAttributeValues;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
	[Authorize(MdmServicePermissions.ItemAttributes.Default)]
	public partial class ItemAttributesAppService : PartialAppService<ItemAttribute, ItemAttributeDto, IItemAttributeRepository>,
		IItemAttributesAppService
	{
		private readonly IItemAttributeRepository _itemAttributeRepository;
		private readonly IItemAttributeValueRepository _itemAttributeValueRepository;
		private readonly IItemRepository _itemRepository;

		public ItemAttributesAppService(ICurrentTenant currentTenant,
			IItemAttributeValueRepository itemAttributeValueRepository,
			IItemAttributeRepository repository,
			IItemRepository itemRepository,
			IConfiguration settingProvider)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.ItemAttributes.Default)
		{
			_itemAttributeRepository = repository;
			_itemAttributeValueRepository = itemAttributeValueRepository;
			_itemRepository = itemRepository;
			_repositories.AddIfNotContains(
				new KeyValuePair<string, object>("IItemAttributeRepository", _itemAttributeRepository));
		}
    }
}
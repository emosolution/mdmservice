using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
	[Authorize(MdmServicePermissions.ItemAttributes.Default)]
	public partial class ItemAttributesAppService : PartialAppService<ItemAttribute, ItemAttributeDto, IItemAttributeRepository>,
		IItemAttributesAppService
	{
		private readonly IItemAttributeRepository _itemAttributeRepository;

		public ItemAttributesAppService(ICurrentTenant currentTenant,
			IItemAttributeRepository repository,
			IConfiguration settingProvider)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.ItemAttributes.Default)
		{
			_itemAttributeRepository = repository;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemAttributeRepository", _itemAttributeRepository));
		}
    }
}
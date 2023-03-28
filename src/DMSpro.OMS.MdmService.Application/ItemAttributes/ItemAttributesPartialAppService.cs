using Volo.Abp.Caching;
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
		private readonly IDistributedCache<ItemAttributeExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly ItemAttributeManager _itemAttributeManager;

		public ItemAttributesAppService(ICurrentTenant currentTenant,
			IItemAttributeRepository repository,
			ItemAttributeManager itemAttributeManager,
			IConfiguration settingProvider,
			IDistributedCache<ItemAttributeExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.ItemAttributes.Default)
		{
			_itemAttributeRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_itemAttributeManager = itemAttributeManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemAttributeRepository", _itemAttributeRepository));
		}
    }
}
using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.Items;

namespace DMSpro.OMS.MdmService.PriceLists
{
	[Authorize(MdmServicePermissions.PriceLists.Default)]
	public partial class PriceListsAppService : PartialAppService<PriceList, PriceListWithDetailsDto, IPriceListRepository>,
		IPriceListsAppService
	{
		private readonly IPriceListRepository _priceListRepository;
		private readonly IDistributedCache<PriceListExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly PriceListManager _priceListManager;
        private readonly IPriceListDetailRepository _priceListDetailRepository;
		private readonly IItemRepository _itemRepository;
        public PriceListsAppService(ICurrentTenant currentTenant,
			IPriceListRepository repository,
			PriceListManager priceListManager,
			IConfiguration settingProvider,
			IPriceListDetailRepository priceListDetailRepository,
			IItemRepository itemRepository,
			IDistributedCache<PriceListExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.PriceLists.Default)
		{
			_priceListRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_priceListManager = priceListManager;
			_priceListDetailRepository = priceListDetailRepository;
			_itemRepository = itemRepository;
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
		}
    }
}
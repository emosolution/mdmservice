using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.Items;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
	[Authorize(MdmServicePermissions.PriceListDetails.Default)]
	public partial class PriceListDetailsAppService : PartialAppService<PriceListDetail, PriceListDetailDto, IPriceListDetailRepository>,
		IPriceListDetailsAppService
	{
		private readonly IPriceListDetailRepository _priceListDetailRepository;
		private readonly IDistributedCache<PriceListDetailExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly PriceListDetailManager _priceListDetailManager;

		private readonly IUOMRepository _uOMRepository;
		private readonly IPriceListRepository _priceListRepository;
		private readonly IItemRepository _itemRepository;

		public PriceListDetailsAppService(ICurrentTenant currentTenant,
			IPriceListDetailRepository repository,
			PriceListDetailManager priceListDetailManager,
			IConfiguration settingProvider,
			IUOMRepository uOMRepository,
			IItemRepository itemRepository,
			IPriceListRepository priceListRepository,
			IDistributedCache<PriceListDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_priceListDetailRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_priceListDetailManager = priceListDetailManager;
			
			_uOMRepository= uOMRepository;
			_itemRepository = itemRepository;	
			_priceListRepository = priceListRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListDetailRepository", _priceListDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IUOMRepository", _uOMRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
            _repositories.AddIfNotContains(
                    new KeyValuePair<string, object>("IItemRepository", _itemRepository));
		}
    }
}
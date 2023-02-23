using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

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

		public PriceListsAppService(ICurrentTenant currentTenant,
			IPriceListRepository repository,
			PriceListManager priceListManager,
			IConfiguration settingProvider,
			IDistributedCache<PriceListExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_priceListRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_priceListManager = priceListManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
		}
    }
}
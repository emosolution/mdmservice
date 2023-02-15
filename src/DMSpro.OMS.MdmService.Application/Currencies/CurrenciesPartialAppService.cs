using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.Currencies
{
	[Authorize(MdmServicePermissions.Currencies.Default)]
	public partial class CurrenciesAppService : PartialAppService<Currency, CurrencyDto, ICurrencyRepository>,
		ICurrenciesAppService
	{
		private readonly ICurrencyRepository _currencyRepository;
		private readonly IDistributedCache<CurrencyExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly CurrencyManager _currencyManager;

		public CurrenciesAppService(ICurrentTenant currentTenant,
			ICurrencyRepository repository,
			CurrencyManager currencyManager,
			IConfiguration settingProvider,
			IDistributedCache<CurrencyExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_currencyRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_currencyManager = currencyManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICurrencyRepository", _currencyRepository));
		}
    }
}
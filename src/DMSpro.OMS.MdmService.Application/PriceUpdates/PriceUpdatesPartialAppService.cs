using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.PriceLists;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    [Authorize(MdmServicePermissions.PriceUpdates.Default)]
    public partial class PriceUpdatesAppService : PartialAppService<PriceUpdate, PriceUpdateDto, IPriceUpdateRepository>,
        IPriceUpdatesAppService
    {
        private readonly IPriceUpdateRepository _priceUpdateRepository;
        private readonly IDistributedCache<PriceUpdateExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly PriceUpdateManager _priceUpdateManager;

        private readonly IPriceListRepository _priceListRepository;

        public PriceUpdatesAppService(ICurrentTenant currentTenant,
            IPriceUpdateRepository repository,
            PriceUpdateManager priceUpdateManager,
            IConfiguration settingProvider,
            IPriceListRepository priceListRepository,
            IDistributedCache<PriceUpdateExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _priceUpdateRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _priceUpdateManager = priceUpdateManager;

            _priceListRepository = priceListRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceUpdateRepository", _priceUpdateRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
        }
    }
}
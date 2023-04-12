using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceUpdateDetails;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    [Authorize(MdmServicePermissions.PriceUpdates.Default)]
    public partial class PriceUpdatesAppService : PartialAppService<PriceUpdate, PriceUpdateWithDetailsDto, IPriceUpdateRepository>,
        IPriceUpdatesAppService
    {
        private readonly IPriceUpdateRepository _priceUpdateRepository;
        private readonly PriceUpdateManager _priceUpdateManager;
        private readonly IPriceUpdateDetailRepository _priceUpdateDetailRepository;
        private readonly IPriceListRepository _priceListRepository;
        private readonly IPriceListDetailRepository _priceListDetailRepository;

        public PriceUpdatesAppService(ICurrentTenant currentTenant,
            IPriceUpdateRepository repository,
            PriceUpdateManager priceUpdateManager,
            IPriceUpdateDetailRepository priceUpdateDetailRepository,
            IPriceListRepository priceListRepository,
            IPriceListDetailRepository priceListDetailRepository,
            IConfiguration settingProvider)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.PriceUpdates.Default)
        {
            _priceUpdateRepository = repository;
            _priceUpdateManager = priceUpdateManager;
            _priceUpdateDetailRepository = priceUpdateDetailRepository;
            _priceListRepository = priceListRepository;
            _priceListDetailRepository = priceListDetailRepository;
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceUpdateRepository", _priceUpdateRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
        }
    }
}
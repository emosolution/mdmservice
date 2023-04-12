using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.PriceListDetails;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    [Authorize(MdmServicePermissions.PriceUpdateDetails.Default)]
    public partial class PriceUpdateDetailsAppService : PartialAppService<PriceUpdateDetail, PriceUpdateDetailWithDetailsDto, IPriceUpdateDetailRepository>,
        IPriceUpdateDetailsAppService
    {
        private readonly IPriceUpdateDetailRepository _priceUpdateDetailRepository;
        private readonly PriceUpdateDetailManager _priceUpdateDetailManager;

        private readonly IPriceUpdateRepository _priceUpdateRepository;
        private readonly IPriceListDetailRepository _priceListDetailRepository;

        public PriceUpdateDetailsAppService(ICurrentTenant currentTenant,
            IPriceUpdateDetailRepository repository,
            PriceUpdateDetailManager priceUpdateDetailManager,
            IConfiguration settingProvider,
            IPriceUpdateRepository priceUpdateRepository,
            IPriceListDetailRepository priceListDetailRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.PriceUpdateDetails.Default)
        {
            _priceUpdateDetailRepository = repository;
            _priceUpdateDetailManager = priceUpdateDetailManager;

            _priceUpdateRepository = priceUpdateRepository;
            _priceListDetailRepository = priceListDetailRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceUpdateDetailRepository", _priceUpdateDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceUpdateRepository", _priceUpdateRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListDetailRepository", _priceListDetailRepository));
        }
    }
}
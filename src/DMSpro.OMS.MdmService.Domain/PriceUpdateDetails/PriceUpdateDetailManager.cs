using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailManager : DomainService
    {
        private readonly IPriceUpdateDetailRepository _priceUpdateDetailRepository;

        public PriceUpdateDetailManager(IPriceUpdateDetailRepository priceUpdateDetailRepository)
        {
            _priceUpdateDetailRepository = priceUpdateDetailRepository;
        }

        public async Task<PriceUpdateDetail> CreateAsync(
            Guid priceUpdateId, Guid priceListDetailId, decimal priceBeforeUpdate, decimal newPrice)
        {
            Check.NotNull(priceUpdateId, nameof(priceUpdateId));
            Check.NotNull(priceListDetailId, nameof(priceListDetailId));

            var priceUpdateDetail = new PriceUpdateDetail(
                GuidGenerator.Create(),
                priceUpdateId, priceListDetailId, priceBeforeUpdate, newPrice, null);

            return await _priceUpdateDetailRepository.InsertAsync(priceUpdateDetail);
        }

        public async Task<PriceUpdateDetail> UpdateAsync(
            Guid id,
            decimal newPrice, 
            [CanBeNull] string concurrencyStamp = null
        )
        {
            var priceUpdateDetail = await _priceUpdateDetailRepository.GetAsync(id);

            priceUpdateDetail.NewPrice = newPrice;

            priceUpdateDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _priceUpdateDetailRepository.UpdateAsync(priceUpdateDetail);
        }
    }
}
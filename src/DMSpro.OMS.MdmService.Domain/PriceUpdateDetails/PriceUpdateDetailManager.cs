using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
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
        Guid priceUpdateId, Guid priceListDetailId, int priceBeforeUpdate, int newPrice, DateTime? updatedDate = null)
        {
            Check.NotNull(priceUpdateId, nameof(priceUpdateId));
            Check.NotNull(priceListDetailId, nameof(priceListDetailId));

            var priceUpdateDetail = new PriceUpdateDetail(
             GuidGenerator.Create(),
             priceUpdateId, priceListDetailId, priceBeforeUpdate, newPrice, updatedDate
             );

            return await _priceUpdateDetailRepository.InsertAsync(priceUpdateDetail);
        }

        public async Task<PriceUpdateDetail> UpdateAsync(
            Guid id,
            Guid priceUpdateId, Guid priceListDetailId, int priceBeforeUpdate, int newPrice, DateTime? updatedDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(priceUpdateId, nameof(priceUpdateId));
            Check.NotNull(priceListDetailId, nameof(priceListDetailId));

            var queryable = await _priceUpdateDetailRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var priceUpdateDetail = await AsyncExecuter.FirstOrDefaultAsync(query);

            priceUpdateDetail.PriceUpdateId = priceUpdateId;
            priceUpdateDetail.PriceListDetailId = priceListDetailId;
            priceUpdateDetail.PriceBeforeUpdate = priceBeforeUpdate;
            priceUpdateDetail.NewPrice = newPrice;
            priceUpdateDetail.UpdatedDate = updatedDate;

            priceUpdateDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _priceUpdateDetailRepository.UpdateAsync(priceUpdateDetail);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailManager : DomainService
    {
        private readonly IPriceListDetailRepository _priceListDetailRepository;

        public PriceListDetailManager(IPriceListDetailRepository priceListDetailRepository)
        {
            _priceListDetailRepository = priceListDetailRepository;
        }

        public async Task<PriceListDetail> CreateAsync(
        Guid priceListId, Guid uOMId, Guid itemId, int price, string description, int? basedOnPrice = null)
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNull(uOMId, nameof(uOMId));
            Check.NotNull(itemId, nameof(itemId));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var priceListDetail = new PriceListDetail(
             GuidGenerator.Create(),
             priceListId, uOMId, itemId, price, description, basedOnPrice
             );

            return await _priceListDetailRepository.InsertAsync(priceListDetail);
        }

        public async Task<PriceListDetail> UpdateAsync(
            Guid id,
            Guid priceListId, Guid uOMId, Guid itemId, int price, string description, int? basedOnPrice = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNull(uOMId, nameof(uOMId));
            Check.NotNull(itemId, nameof(itemId));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var queryable = await _priceListDetailRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var priceListDetail = await AsyncExecuter.FirstOrDefaultAsync(query);

            priceListDetail.PriceListId = priceListId;
            priceListDetail.UOMId = uOMId;
            priceListDetail.ItemId = itemId;
            priceListDetail.Price = price;
            priceListDetail.Description = description;
            priceListDetail.BasedOnPrice = basedOnPrice;

            priceListDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _priceListDetailRepository.UpdateAsync(priceListDetail);
        }

    }
}
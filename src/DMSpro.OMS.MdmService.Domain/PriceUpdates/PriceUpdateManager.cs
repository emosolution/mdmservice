using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateManager : DomainService
    {
        private readonly IPriceUpdateRepository _priceUpdateRepository;

        public PriceUpdateManager(IPriceUpdateRepository priceUpdateRepository)
        {
            _priceUpdateRepository = priceUpdateRepository;
        }

        public async Task<PriceUpdate> CreateAsync(
            Guid priceListId, string code, string description, bool isScheduled,
            DateTime? effectiveDate = null, DateTime? endDate = null)
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), PriceUpdateConsts.CodeMaxLength, PriceUpdateConsts.CodeMinLength);
            Check.Length(description, nameof(description), PriceUpdateConsts.DescriptionMaxLength);

            var priceUpdate = new PriceUpdate(
                GuidGenerator.Create(),
                priceListId, code, description, PriceUpdateStatus.OPEN, isScheduled,
                effectiveDate, endDate, null, null, null);

            return await _priceUpdateRepository.InsertAsync(priceUpdate);
        }

        public async Task<PriceUpdate> UpdateAsync(
            Guid id,
            string description, bool isScheduled,
            DateTime? effectiveDate = null, DateTime? endDate = null,
            [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.Length(description, nameof(description), PriceUpdateConsts.DescriptionMaxLength);

            var priceUpdate = await _priceUpdateRepository.GetAsync(id);

            priceUpdate.Description = description;
            priceUpdate.IsScheduled = isScheduled;
            priceUpdate.EffectiveDate = effectiveDate;
            priceUpdate.EndDate = endDate;

            priceUpdate.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _priceUpdateRepository.UpdateAsync(priceUpdate);
        }
    }
}
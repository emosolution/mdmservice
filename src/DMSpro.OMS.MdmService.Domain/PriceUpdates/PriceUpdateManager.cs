using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
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
        Guid priceListId, string code, string description, DateTime effectiveDate, PriceUpdateStatus status, DateTime? updateStatusDate = null)
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), PriceUpdateConsts.CodeMaxLength, PriceUpdateConsts.CodeMinLength);
            Check.Length(description, nameof(description), PriceUpdateConsts.DescriptionMaxLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));
            Check.NotNull(status, nameof(status));

            var priceUpdate = new PriceUpdate(
             GuidGenerator.Create(),
             priceListId, code, description, effectiveDate, status, updateStatusDate
             );

            return await _priceUpdateRepository.InsertAsync(priceUpdate);
        }

        public async Task<PriceUpdate> UpdateAsync(
            Guid id,
            Guid priceListId, string code, string description, DateTime effectiveDate, PriceUpdateStatus status, DateTime? updateStatusDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), PriceUpdateConsts.CodeMaxLength, PriceUpdateConsts.CodeMinLength);
            Check.Length(description, nameof(description), PriceUpdateConsts.DescriptionMaxLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));
            Check.NotNull(status, nameof(status));

            var priceUpdate = await _priceUpdateRepository.GetAsync(id);

            priceUpdate.PriceListId = priceListId;
            priceUpdate.Code = code;
            priceUpdate.Description = description;
            priceUpdate.EffectiveDate = effectiveDate;
            priceUpdate.Status = status;
            priceUpdate.UpdateStatusDate = updateStatusDate;

            priceUpdate.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _priceUpdateRepository.UpdateAsync(priceUpdate);
        }

    }
}
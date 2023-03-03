using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.Currencies
{
    public class CurrencyManager : DomainService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyManager(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<Currency> CreateAsync(
        string code, string name)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CurrencyConsts.CodeMaxLength, CurrencyConsts.CodeMinLength);
            Check.Length(name, nameof(name), CurrencyConsts.NameMaxLength);

            var currency = new Currency(
             GuidGenerator.Create(),
             code, name
             );

            return await _currencyRepository.InsertAsync(currency);
        }

        public async Task<Currency> UpdateAsync(
            Guid id,
            string code, string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CurrencyConsts.CodeMaxLength, CurrencyConsts.CodeMinLength);
            Check.Length(name, nameof(name), CurrencyConsts.NameMaxLength);

            var currency = await _currencyRepository.GetAsync(id);

            currency.Code = code;
            currency.Name = name;

            currency.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _currencyRepository.UpdateAsync(currency);
        }

    }
}
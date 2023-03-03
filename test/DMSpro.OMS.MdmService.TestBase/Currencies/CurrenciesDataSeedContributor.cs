using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.Currencies;

namespace DMSpro.OMS.MdmService.Currencies
{
    public class CurrenciesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CurrenciesDataSeedContributor(ICurrencyRepository currencyRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _currencyRepository = currencyRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _currencyRepository.InsertAsync(new Currency
            (
                id: Guid.Parse("90d6126f-c3e7-45d4-9cae-4898bcb313cd"),
                code: "f8e59fa359dd41d8984b",
                name: "ed63c435c5cc44b8880537f259d294a0152a95cac8a64d859c80e6d5bfd9208e0910a330cc954fc88d5bee52fc2cf83db142"
            ));

            await _currencyRepository.InsertAsync(new Currency
            (
                id: Guid.Parse("9b21e357-66b2-4b0c-a7a5-e01349305c45"),
                code: "42772b749dce4cceaa33",
                name: "c71b5ce289354dfe907bcaf95e89d909add159bdbb6641d388dd1b98de9d459ad39693b6a89a4fbeb668b64fad9e4bbb4cec"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
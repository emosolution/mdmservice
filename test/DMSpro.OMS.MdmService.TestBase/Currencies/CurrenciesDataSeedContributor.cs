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
                id: Guid.Parse("b9db60e3-0dc5-4b87-be00-55e2fbf01676"),
                code: "0c7175bbe58d42db9133",
                name: "bc05aa92eadd42a19c098ffd52269cd95716eba9eff7452fa3de16a7719e529d788f4fc4c7034f08934d3495b875f87a8181"
            ));

            await _currencyRepository.InsertAsync(new Currency
            (
                id: Guid.Parse("f879d4a6-0695-4106-ae96-fadbc0e3fe81"),
                code: "59d29f6da1024153bf34",
                name: "0ba4c5bb6865482889d580c6fe4c6adc03be00d6b13640689d9c2e42822ab871055670ef8959490b9e5f0821047bd0634d9f"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
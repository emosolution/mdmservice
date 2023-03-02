using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPriceListRepository _priceListRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly PriceListsDataSeedContributor _priceListsDataSeedContributor;

        public PriceListsDataSeedContributor(IPriceListRepository priceListRepository, IUnitOfWorkManager unitOfWorkManager, PriceListsDataSeedContributor priceListsDataSeedContributor)
        {
            _priceListRepository = priceListRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _priceListsDataSeedContributor = priceListsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _priceListsDataSeedContributor.SeedAsync(context);

            await _priceListRepository.InsertAsync(new PriceList
            (
                id: Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                code: "d9d2946452f041b2b48a",
                name: "6ffddfa107fa4eddbed864ea1b854cc50efb5942fec94a29909cdec142f89d9a6f9b06bf27fb4c4397a1959bfff84402a779143cb48042878681d105365b8d1edf57865fa57a4b018db4b7e88705b4bfddd644fd49f24e038692317f089c306be059b1ded5544995ba063fd33cccff99c07b188f6f004c8cb256bea6c262582",
                active: true,
                arithmeticOperation: default,
                arithmeticFactor: 995660339,
                arithmeticFactorType: default,
                isFirstPriceList: true,
                basePriceListId: null
            ));

            await _priceListRepository.InsertAsync(new PriceList
            (
                id: Guid.Parse("427c26f4-3870-49b3-be11-da4cff578df6"),
                code: "9acbea757ad249569623",
                name: "bec111dd2dab46df81e97f6d9221501ee1c13aff5b1e4e38bc2e4be8970d178fe3bea3af110a4a9db5d95bc492f39588f27f1e0eb16b47cbbb7e2aa879b9bcce340cdf87db474038b4d42f4eb45ded5dc874e3b6da6f4178a3bd82c43ba4b35469946e3c4cee4d55bacab0ad491e3409d99fd9b83b4e4a08b4a07f6d100405a",
                active: true,
                arithmeticOperation: default,
                arithmeticFactor: 363999217,
                arithmeticFactorType: default,
                isFirstPriceList: true,
                basePriceListId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
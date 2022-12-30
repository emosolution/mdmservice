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
                id: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                code: "1f00af0790344a42850c",
                name: "60253ec",
                active: true,
                arithmeticOperation: default,
                arithmeticFactor: 1142738050,
                arithmeticFactorType: default,
                isFirstPriceList: true,
                basePriceListId: null
            ));

            await _priceListRepository.InsertAsync(new PriceList
            (
                id: Guid.Parse("93093ff1-5104-4d11-b8e1-a3d31bc38aa7"),
                code: "da37f73422dc4d69b358",
                name: "2874a318b89b4b7794bbdca412a5f660fb62df319c954a0f8cb2",
                active: true,
                arithmeticOperation: default,
                arithmeticFactor: 2076597479,
                arithmeticFactorType: default,
                isFirstPriceList: true,
                basePriceListId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
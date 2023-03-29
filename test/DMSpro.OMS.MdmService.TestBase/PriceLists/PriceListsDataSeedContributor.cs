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
                id: Guid.Parse("e12fe94e-91f7-474f-a74b-c83fc1fce713"),
                code: "051f028eca5c48219a62",
                name: "3988047974bc4b3792a326e64b903099933b84a5c5424e99a783e1c6384ea19c3a490be50bc44f38aed2f05bc16409c659df9f5c4b244b6f91b31109bf32dc7b2ee1494ded7b44a09fbf04ad9a6b4056a37ebf64717a4e01b1690b0a61ca414bec5eb504eb8a4e118fc537290c5e38022879a972cee84de18b39642478deda1",
                active: true,
                arithmeticOperation: default,
                arithmeticFactor: 1059156570,
                arithmeticFactorType: default,
                isBase: true,
                isDefault: true,
                isReleased: true,
                releasedDate: new DateTime(2004, 10, 18),
                basePriceListId: null
            ));

            await _priceListRepository.InsertAsync(new PriceList
            (
                id: Guid.Parse("30ca8014-04a5-4c6f-8771-7cee23ada2fa"),
                code: "12cc1d16fe874594943e",
                name: "50839c9ba074473981cf9c74437834eb8cc36569bded4504842ea752c3848c7af5d70f05b1494077beef01513c007a49bee67b7ac5004a96853d28256311aaef43be7b94ae834da89e791efb3b52f63f49123de283f54ee6af5b0d4241ad790692bd85868a3c4ee98c78ceb432a7e2c489ff57a97b79445993db34d2d0a3bad",
                active: true,
                arithmeticOperation: default,
                arithmeticFactor: 973365952,
                arithmeticFactorType: default,
                isBase: true,
                isDefault: true,
                isReleased: true,
                releasedDate: new DateTime(2007, 1, 3),
                basePriceListId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
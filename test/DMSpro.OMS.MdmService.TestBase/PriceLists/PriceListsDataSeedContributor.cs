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
                id: Guid.Parse("3158ddf9-eb6a-4e20-b24f-7c0161437270"),
                code: "6685b3a724e84078b17f",
                name: "4673fdda15d645caa2eb39b91ccd63db541f3d66f3e446b39b217f7b562a1a3d9eaaeb67ffdd434e91cd29283d9778b0c6cb4cd127a140da829b2f03c33d68ac81d7f2a4bd44441b996c456b98b7e2af6975d893ffb940a694b76cc7328d1000fa0802b5cfad4f1bb1aa45802e2287e4e13d40eca7d4449fb0bc89d916baf61",
                active: true,
                arithmeticOperation: default,
                arithmeticFactor: 1110588862,
                arithmeticFactorType: default,
                isBase: true,
                isDefault: true,
                basePriceListId: null
            ));

            await _priceListRepository.InsertAsync(new PriceList
            (
                id: Guid.Parse("27dfeab2-4d68-47a4-8a41-43a472f9fb23"),
                code: "5c58dc875c5e48d4b5de",
                name: "06c77106bd414e638a806220e1c4de9d8565a70928594d3abb290d557b3a15efdf39365517a84d5a82979a65ceab71f81d78e78326f9463abc0f294b04a3a5f3266f5959990c42309f9a1098373d4e31905c18a6fd554def96275a585dafa2456e6fb4a84ac0403282b73873fb9f629541938fd0b3574d24ac439cd7a7fb749",
                active: true,
                arithmeticOperation: default,
                arithmeticFactor: 2040105952,
                arithmeticFactorType: default,
                isBase: true,
                isDefault: true,
                basePriceListId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
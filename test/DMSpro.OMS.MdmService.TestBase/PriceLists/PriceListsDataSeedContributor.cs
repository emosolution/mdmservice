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
                id: Guid.Parse("4758593c-d51b-4934-a996-ee0572f5c083"),
                code: "585fa4690a8342209718",
                name: "726cdcfb679b4615865c0c045ded84169a60fd8529414b7f878cd401781ca2044501a56f82ad419b9c642e606750b848e2803bb835f84ad39dfdf0536e0c58313aea2d83572b4569b189f5344b1185295273943c6de44fa98d85cc2804edcbd35012fc491472421486ceb556e9be4b0798d3101e95df43dbaf2065a1231c33f",
                active: true,
                arithmeticOperation: default,
                arithmeticFactor: 897589903,
                arithmeticFactorType: default,
                isBase: true,
                isDefaultForCustomer: true,
                isReleased: true,
                releasedDate: new DateTime(2010, 9, 1),
                isDefaultForVendor: true,
                basePriceListId: null
            ));

            await _priceListRepository.InsertAsync(new PriceList
            (
                id: Guid.Parse("e6614a70-bc85-46e1-ae4c-773eb87c843e"),
                code: "302b04a1887842b8bae2",
                name: "13fd22a4dd8b46a0bd59124269730154ace9ad6b055c4204b3de792e5188d79b3177db3ed3804d9b8ddde12fb2357e59b45746c8357e43f494a82d06281735464928c3efad49467b9eba1b4b8d669e6bdffe554dcf114e97b46313e51a977be842ac6f461234435282808fe871b8e3c2ea9481c2788d4042980f09fae8e6785",
                active: true,
                arithmeticOperation: default,
                arithmeticFactor: 1111679306,
                arithmeticFactorType: default,
                isBase: true,
                isDefaultForCustomer: true,
                isReleased: true,
                releasedDate: new DateTime(2005, 11, 21),
                isDefaultForVendor: true,
                basePriceListId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
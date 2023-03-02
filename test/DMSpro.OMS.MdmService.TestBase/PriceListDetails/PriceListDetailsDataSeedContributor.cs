using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.PriceListDetails;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPriceListDetailRepository _priceListDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly PriceListsDataSeedContributor _priceListsDataSeedContributor;

        private readonly UOMsDataSeedContributor _uOMsDataSeedContributor;

        private readonly ItemsDataSeedContributor _itemsDataSeedContributor;

        public PriceListDetailsDataSeedContributor(IPriceListDetailRepository priceListDetailRepository, IUnitOfWorkManager unitOfWorkManager, PriceListsDataSeedContributor priceListsDataSeedContributor, UOMsDataSeedContributor uOMsDataSeedContributor, ItemsDataSeedContributor itemsDataSeedContributor)
        {
            _priceListDetailRepository = priceListDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _priceListsDataSeedContributor = priceListsDataSeedContributor; _uOMsDataSeedContributor = uOMsDataSeedContributor; _itemsDataSeedContributor = itemsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _priceListsDataSeedContributor.SeedAsync(context);
            await _uOMsDataSeedContributor.SeedAsync(context);
            await _itemsDataSeedContributor.SeedAsync(context);

            await _priceListDetailRepository.InsertAsync(new PriceListDetail
            (
                id: Guid.Parse("6613d68d-298d-441e-9716-5eb1f3143ae9"),
                price: 490698435,
                basedOnPrice: 1471405567,
                description: "5bf6f0554756476ca464437e2806735a19e38123",
                priceListId: Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                uOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                itemId: Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4")
            ));

            await _priceListDetailRepository.InsertAsync(new PriceListDetail
            (
                id: Guid.Parse("7e7e2c2f-264b-49c6-8052-25bcd131f787"),
                price: 1615523586,
                basedOnPrice: 742160364,
                description: "751cb588f908496992e1d99d29f1caabb9eaf3e03b93457ba5efc7aa027e418bff1951f3bf1b4c9693",
                priceListId: Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                uOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                itemId: Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
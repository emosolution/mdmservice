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
                id: Guid.Parse("0e4184d8-69c6-4e16-90a4-c44f3d08337a"),
                price: 540001679,
                basedOnPrice: 990352987,
                description: "d097712be59d450f937ca72bcb5554df0b277ef715d448448e261b7dd3b077935581e78e954f43c780c494dad85",
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                uOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                itemId: Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            ));

            await _priceListDetailRepository.InsertAsync(new PriceListDetail
            (
                id: Guid.Parse("814e2f11-06f4-45cf-9e36-80df209fbc15"),
                price: 1394487410,
                basedOnPrice: 947230766,
                description: "16589b337a17494c88f35913b5daa0d9c89d3d369fdc4ee1b23",
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                uOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                itemId: Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.ItemMasters;
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

        private readonly ItemMastersDataSeedContributor _itemMastersDataSeedContributor;

        private readonly UOMsDataSeedContributor _uOMsDataSeedContributor;

        public PriceListDetailsDataSeedContributor(IPriceListDetailRepository priceListDetailRepository, IUnitOfWorkManager unitOfWorkManager, PriceListsDataSeedContributor priceListsDataSeedContributor, ItemMastersDataSeedContributor itemMastersDataSeedContributor, UOMsDataSeedContributor uOMsDataSeedContributor)
        {
            _priceListDetailRepository = priceListDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _priceListsDataSeedContributor = priceListsDataSeedContributor; _itemMastersDataSeedContributor = itemMastersDataSeedContributor; _uOMsDataSeedContributor = uOMsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _priceListsDataSeedContributor.SeedAsync(context);
            await _itemMastersDataSeedContributor.SeedAsync(context);
            await _uOMsDataSeedContributor.SeedAsync(context);

            await _priceListDetailRepository.InsertAsync(new PriceListDetail
            (
                id: Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46"),
                price: 140043996,
                basedOnPrice: 537506613,
                description: "8ac9e1451f3849c0af1d3f",
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                itemMasterId: Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464"),
                uOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            ));

            await _priceListDetailRepository.InsertAsync(new PriceListDetail
            (
                id: Guid.Parse("be683c3f-83de-422b-8e19-34743edd5107"),
                price: 517560199,
                basedOnPrice: 995827501,
                description: "b69cba5b86aa48cf8ea8440f792e294a69c238b342fa405e9862da18d780d7466271",
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                itemMasterId: Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464"),
                uOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
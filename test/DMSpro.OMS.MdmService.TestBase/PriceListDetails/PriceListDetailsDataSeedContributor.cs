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

        public PriceListDetailsDataSeedContributor(IPriceListDetailRepository priceListDetailRepository, IUnitOfWorkManager unitOfWorkManager, PriceListsDataSeedContributor priceListsDataSeedContributor, UOMsDataSeedContributor uOMsDataSeedContributor)
        {
            _priceListDetailRepository = priceListDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _priceListsDataSeedContributor = priceListsDataSeedContributor; _uOMsDataSeedContributor = uOMsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _priceListsDataSeedContributor.SeedAsync(context);
            await _uOMsDataSeedContributor.SeedAsync(context);

            await _priceListDetailRepository.InsertAsync(new PriceListDetail
            (
                id: Guid.Parse("f8268675-0260-4229-ab59-b4900bead351"),
                price: 1644042918,
                basedOnPrice: 1416648660,
                description: "d76f06c53cce4b66b273813284f9a4c120f40e38bd034b2ca0260176",
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                uomId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            ));

            await _priceListDetailRepository.InsertAsync(new PriceListDetail
            (
                id: Guid.Parse("161f23e8-de0a-4c06-9e8e-7a265acf0803"),
                price: 1636551312,
                basedOnPrice: 409292364,
                description: "f23907d612f14b05ad4007c1f96b7f1af7d9fe910fdd4547b60904846d7d351176c2d6faf3a64a6a8c2d282c5",
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                uomId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
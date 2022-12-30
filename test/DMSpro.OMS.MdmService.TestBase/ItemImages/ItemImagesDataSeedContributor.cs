using DMSpro.OMS.MdmService.ItemMasters;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.ItemImages;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImagesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemImageRepository _itemImageRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ItemMastersDataSeedContributor _itemMastersDataSeedContributor;

        public ItemImagesDataSeedContributor(IItemImageRepository itemImageRepository, IUnitOfWorkManager unitOfWorkManager, ItemMastersDataSeedContributor itemMastersDataSeedContributor)
        {
            _itemImageRepository = itemImageRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _itemMastersDataSeedContributor = itemMastersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _itemMastersDataSeedContributor.SeedAsync(context);

            await _itemImageRepository.InsertAsync(new ItemImage
            (
                id: Guid.Parse("cda3773d-9602-4a0e-b53e-7be1ba1f59f6"),
                description: "949e8480d7b441d0a0409838a7fb9546e7aaddb95bca4bdcaeb88d4b8b0adb6ee0ddf89741664f368948",
                active: true,
                uRL: "2ae783dcdf",
                displayOrder: 1252920544,
                itemId: Guid.Parse("846548f8-0a9b-4ff6-9831-bdc654dbdf64")
            ));

            await _itemImageRepository.InsertAsync(new ItemImage
            (
                id: Guid.Parse("c4802d68-4559-4852-b8f0-e399a2b5ba7c"),
                description: "dd3067b3645744b48e0e3eb49bf858f2f78695024af94d98ab6a7650dd",
                active: true,
                uRL: "7610791fd19c48519ad7b77630b7a8bec0cd66f50a124f09affbfa922df814d5",
                displayOrder: 1917553115,
                itemId: Guid.Parse("846548f8-0a9b-4ff6-9831-bdc654dbdf64")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
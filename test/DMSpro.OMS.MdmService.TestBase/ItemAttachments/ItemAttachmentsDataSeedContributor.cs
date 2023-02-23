using DMSpro.OMS.MdmService.Items;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.ItemAttachments;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemAttachmentRepository _itemAttachmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ItemsDataSeedContributor _itemsDataSeedContributor;

        public ItemAttachmentsDataSeedContributor(IItemAttachmentRepository itemAttachmentRepository, IUnitOfWorkManager unitOfWorkManager, ItemsDataSeedContributor itemsDataSeedContributor)
        {
            _itemAttachmentRepository = itemAttachmentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _itemsDataSeedContributor = itemsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _itemsDataSeedContributor.SeedAsync(context);

            await _itemAttachmentRepository.InsertAsync(new ItemAttachment
            (
                id: Guid.Parse("6ca6e468-ab4f-43c5-b376-dd94d92b830a"),
                description: "ac3dcd22c821479890e1346d0da16addc14ac935df6c4614b33594c84c2233ab688090769fbb4c3f925bf2cee9e536eb3b00ca65747c4e6b91caaae2405ddf093558a4e61e4941c39b41ca373536a326825fa18830854d46a90537ea900c3348c9241ed07a2144fab1c5096205c7c0e5915ecae8d34f405b8ba375f2fa9aa289bf61fa08b4b74e4086b60a3236a706d9e7a9db14dbde4491a7d287d661666e86472cf6629a7d4fd9a2320fa625b320e055aafa33da4a421d8c847852ffce5c8db2d2d3b641824779a6dbc9830609bdd35a7e7869720c464e9363cb09945329a49938f9769a754f79ba4744bb89f8882c575d3401544040899524",
                active: true,
                fileId: Guid.Parse("5d3fcbba-6345-48be-b378-1af74795f2cd"),
                itemId: Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            ));

            await _itemAttachmentRepository.InsertAsync(new ItemAttachment
            (
                id: Guid.Parse("0af5b120-f029-44a4-b0b5-bb4fad5baf6b"),
                description: "a079da680b304a75a56d537775b7484fa1dce24edfc24b3396a304ce2c7e244621065d171fd84161bbd47b970839f8b50b80a39d75044513b9b251dc6b34f4a817b77efb40b84736b6691a55f4a849c5137ee3e25bb34777972908de354da6c0790ab3d766c74ac99987f266251d3a4d6a32bbb1420a453bb6c892a1dce46cb4391737d546fa419db948163aa1d9798e97ba22933dd54481b2b352aa51e5f7cb3baaf9a4cb6e4ae190c40bfe01388b2283af43f7a2094af389cce163e606360ffc125820a447413da5de3fa012d63d740cca2c355d2543f6aa618897d6cdd7c11881844a96c142c5887abbbf6634db80bda5192ae8874ec78be5",
                active: true,
                fileId: Guid.Parse("8defe46e-6102-403e-b711-6931112910dd"),
                itemId: Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
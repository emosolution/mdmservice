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
                id: Guid.Parse("085e25a9-91ba-48b8-9d01-2d28a6f69d72"),
                description: "811ce6c9542e4387b51ba01cceb85730872b292977e0449cab67343d686187f550c35055e0c14c07b238fbb4aac0655dc0c36e93b292470a9544afa9dbaad41a32f0f37d6cde4b35ad6ac347ae7f7bdcd834cd283c1f45319fb54d0fa112d0ee0b9a30a0a272420aabf7c7aa82705e2d197920d5f770470daf5cf25d660dae134a4ae19d04a34f78979a0a9b0c9ded6ecfd62f8e9e944522871235f37765c28bacaa797f26cf402fb96c0a8e86a13b033fac66686b3244c5ae3a4dff1260867f910ddebc87664dd69c5cf2961e11f8e15f9846d55a3b4b08866151ea19d66b2d12986a5ea900454298b858f58cbbae839f41d919f85a4cdaa126",
                url: "15364f0dd618485493abdc652bb9b7674bc285c5e90843058ce2436f86f0116aa3fb41ccc3f84a09b44238f89b2dee12967e3332400a47cd8d42ed75ba679e5ffa9009a8e9d14e9ca5e286d1f1a42a9ca5c3f1d80d054348afc0a8eeba04cacfdfe7eb2c4d84470b8539292b3fce5320c3d56401422143cfa05d3a2f23add444462df25bdcb441baaea06a9db9fa324b4132c0a722014e0a90ebc271f86872497dd9fffa9a334cb4a1855a559bcd643432f42e5d7ab3446e9f8fdbd99b551744e77402d1522746a3afb3637875f0c7412fdb9464ed2c45da88dde992a166bebecc674b80cb8444819eb2c86281f0a7381f6cd136e6f34865975c",
                active: true,
                itemId: Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            ));

            await _itemAttachmentRepository.InsertAsync(new ItemAttachment
            (
                id: Guid.Parse("cf6c4f27-336d-4daa-af74-ec751e244a7b"),
                description: "398f0f9afd55486b98c8a88e9a9bb3b7e70d619958d7487c9a43e1553662885f9539e33a7cd449df9f72189b0d2d4f3f077898b01ffe4c4c91623b755cbbf86a468ec92083d14dd4b441d6679a0a802adb1a99c3e14a48138d3d431cb013a9b185b58941441d483f82beba95705898a53ab8472549444c7abd2f63bbf5e784cec0aa33618730406085c2f8b0e4244289458651cce9a44f0e859a54c4ef6f3b628f1e4aec8e144a9d99305593b9069821171e249a13d44a74a262f1ee7829594b002b317012404244bffd41367904a9d99d198937d7a949328f0756278419051dda3f3b84f24d4d429f19022190c857f800d80084eff14fc69752",
                url: "4d5274cdb6d04cdf93414fdd4292b30a8c3e9443944d482bb81694552b258e8a083df9621f854c11af813a149e6575e93b286158a6fd49c5929f07f5eeaaf3fa4e9c9088d6734daba8ab22153cae917026ebeeae30184af497a9e0d649388ebfde40f0b374f9480d85487f661dedd8868481b793063740e7b196ed51a58d68a94f8dc9a1bbe641b28a60c42787fe46b56c6d2d0bd5424ce094815072ac3f649ff201f00c22914bc5b080761893498b2c28d9d0da8192498b9693078758793a4ae6c5ff19a1a9406c8ed683a389b3af0e261a0b1f2eb94afabd12202f50784147d61dc7d9a3d54ee39c8411cb32c3029fded0a12015114b87bac0",
                active: true,
                itemId: Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
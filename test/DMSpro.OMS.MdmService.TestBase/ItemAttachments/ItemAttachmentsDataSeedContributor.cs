using DMSpro.OMS.MdmService.ItemMasters;
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
        private readonly ItemMastersDataSeedContributor _itemMastersDataSeedContributor;

        public ItemAttachmentsDataSeedContributor(IItemAttachmentRepository itemAttachmentRepository, IUnitOfWorkManager unitOfWorkManager, ItemMastersDataSeedContributor itemMastersDataSeedContributor)
        {
            _itemAttachmentRepository = itemAttachmentRepository;
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

            await _itemAttachmentRepository.InsertAsync(new ItemAttachment
            (
                id: Guid.Parse("0f8663b1-558e-4a85-aa76-6a1d1f6b3acf"),
                description: "36d36b0b79bd42339a70a25c29128ebb3d129bfdd4e8404f9a1950b7c5fce5305c39040a38244545adf307493398",
                active: true,
                uRL: "48a86c0ff0bd4781970730fcf88b79280737",
                itemId: Guid.Parse("846548f8-0a9b-4ff6-9831-bdc654dbdf64")
            ));

            await _itemAttachmentRepository.InsertAsync(new ItemAttachment
            (
                id: Guid.Parse("40854521-e646-431a-91a3-dc97e43da96f"),
                description: "853c135e2f074a2f9922367046cf5d22e1ea9c599b",
                active: true,
                uRL: "fa9da426f51a4c4f9c2cbf620b2acb170fe16e0f5d8840cf84de8a7f451bcc44e4dec7e50a744ca0be83df6cfb1e74",
                itemId: Guid.Parse("846548f8-0a9b-4ff6-9831-bdc654dbdf64")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}
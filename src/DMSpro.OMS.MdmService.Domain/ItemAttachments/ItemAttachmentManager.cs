using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentManager : DomainService
    {
        private readonly IItemAttachmentRepository _itemAttachmentRepository;

        public ItemAttachmentManager(IItemAttachmentRepository itemAttachmentRepository)
        {
            _itemAttachmentRepository = itemAttachmentRepository;
        }

        public async Task<ItemAttachment> CreateAsync(
        Guid itemId, string description, bool active, Guid fileId)
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.Length(description, nameof(description), ItemAttachmentConsts.DescriptionMaxLength);

            var itemAttachment = new ItemAttachment(
             GuidGenerator.Create(),
             itemId, description, active, fileId
             );

            return await _itemAttachmentRepository.InsertAsync(itemAttachment);
        }

        public async Task<ItemAttachment> UpdateAsync(
            Guid id,
            Guid itemId, string description, bool active, Guid fileId, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.Length(description, nameof(description), ItemAttachmentConsts.DescriptionMaxLength);

            var itemAttachment = await _itemAttachmentRepository.GetAsync(id);

            itemAttachment.ItemId = itemId;
            itemAttachment.Description = description;
            itemAttachment.Active = active;
            itemAttachment.FileId = fileId;

            itemAttachment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemAttachmentRepository.UpdateAsync(itemAttachment);
        }

    }
}
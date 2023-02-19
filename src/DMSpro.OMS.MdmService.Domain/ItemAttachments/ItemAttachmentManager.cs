using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
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
        Guid itemId, string description, string url, bool active, Guid fileId)
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.Length(description, nameof(description), ItemAttachmentConsts.DescriptionMaxLength);
            Check.NotNullOrWhiteSpace(url, nameof(url));
            Check.Length(url, nameof(url), ItemAttachmentConsts.UrlMaxLength, ItemAttachmentConsts.UrlMinLength);

            var itemAttachment = new ItemAttachment(
             GuidGenerator.Create(),
             itemId, description, url, active, fileId
             );

            return await _itemAttachmentRepository.InsertAsync(itemAttachment);
        }

        public async Task<ItemAttachment> UpdateAsync(
            Guid id,
            Guid itemId, string description, string url, bool active, Guid fileId, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.Length(description, nameof(description), ItemAttachmentConsts.DescriptionMaxLength);
            Check.NotNullOrWhiteSpace(url, nameof(url));
            Check.Length(url, nameof(url), ItemAttachmentConsts.UrlMaxLength, ItemAttachmentConsts.UrlMinLength);

            var itemAttachment = await _itemAttachmentRepository.GetAsync(id);

            itemAttachment.ItemId = itemId;
            itemAttachment.Description = description;
            itemAttachment.Url = url;
            itemAttachment.Active = active;
            itemAttachment.FileId = fileId;

            itemAttachment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemAttachmentRepository.UpdateAsync(itemAttachment);
        }

    }
}
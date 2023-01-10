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
        Guid itemId, string description, string url, bool active)
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.Length(description, nameof(description), ItemAttachmentConsts.DescriptionMaxLength);
            Check.NotNullOrWhiteSpace(url, nameof(url));
            Check.Length(url, nameof(url), ItemAttachmentConsts.UrlMaxLength, ItemAttachmentConsts.UrlMinLength);

            var itemAttachment = new ItemAttachment(
             GuidGenerator.Create(),
             itemId, description, url, active
             );

            return await _itemAttachmentRepository.InsertAsync(itemAttachment);
        }

        public async Task<ItemAttachment> UpdateAsync(
            Guid id,
            Guid itemId, string description, string url, bool active, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.Length(description, nameof(description), ItemAttachmentConsts.DescriptionMaxLength);
            Check.NotNullOrWhiteSpace(url, nameof(url));
            Check.Length(url, nameof(url), ItemAttachmentConsts.UrlMaxLength, ItemAttachmentConsts.UrlMinLength);

            var queryable = await _itemAttachmentRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var itemAttachment = await AsyncExecuter.FirstOrDefaultAsync(query);

            itemAttachment.ItemId = itemId;
            itemAttachment.Description = description;
            itemAttachment.Url = url;
            itemAttachment.Active = active;

            itemAttachment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemAttachmentRepository.UpdateAsync(itemAttachment);
        }

    }
}
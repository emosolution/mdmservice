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
        Guid itemId, string description, bool active, string uRL)
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.NotNullOrWhiteSpace(uRL, nameof(uRL));

            var itemAttachment = new ItemAttachment(
             GuidGenerator.Create(),
             itemId, description, active, uRL
             );

            return await _itemAttachmentRepository.InsertAsync(itemAttachment);
        }

        public async Task<ItemAttachment> UpdateAsync(
            Guid id,
            Guid itemId, string description, bool active, string uRL, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.NotNullOrWhiteSpace(uRL, nameof(uRL));

            var queryable = await _itemAttachmentRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var itemAttachment = await AsyncExecuter.FirstOrDefaultAsync(query);

            itemAttachment.ItemId = itemId;
            itemAttachment.Description = description;
            itemAttachment.Active = active;
            itemAttachment.URL = uRL;

            itemAttachment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemAttachmentRepository.UpdateAsync(itemAttachment);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageManager : DomainService
    {
        private readonly IItemImageRepository _itemImageRepository;

        public ItemImageManager(IItemImageRepository itemImageRepository)
        {
            _itemImageRepository = itemImageRepository;
        }

        public async Task<ItemImage> CreateAsync(
        Guid itemId, string description, bool active, int displayOrder, Guid fileId)
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.Length(description, nameof(description), ItemImageConsts.DescriptionMaxLength);

            var itemImage = new ItemImage(
             GuidGenerator.Create(),
             itemId, description, active, displayOrder, fileId
             );

            return await _itemImageRepository.InsertAsync(itemImage);
        }

        public async Task<ItemImage> UpdateAsync(
            Guid id,
            Guid itemId, string description, bool active, int displayOrder, Guid fileId, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.Length(description, nameof(description), ItemImageConsts.DescriptionMaxLength);

            var itemImage = await _itemImageRepository.GetAsync(id);

            itemImage.ItemId = itemId;
            itemImage.Description = description;
            itemImage.Active = active;
            itemImage.DisplayOrder = displayOrder;
            itemImage.FileId = fileId;

            itemImage.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemImageRepository.UpdateAsync(itemImage);
        }

    }
}
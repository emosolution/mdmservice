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
        Guid itemId, string description, string url, bool active, int displayOrder)
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.Length(description, nameof(description), ItemImageConsts.DescriptionMaxLength);
            Check.NotNullOrWhiteSpace(url, nameof(url));
            Check.Length(url, nameof(url), ItemImageConsts.UrlMaxLength, ItemImageConsts.UrlMinLength);

            var itemImage = new ItemImage(
             GuidGenerator.Create(),
             itemId, description, url, active, displayOrder
             );

            return await _itemImageRepository.InsertAsync(itemImage);
        }

        public async Task<ItemImage> UpdateAsync(
            Guid id,
            Guid itemId, string description, string url, bool active, int displayOrder, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemId, nameof(itemId));
            Check.Length(description, nameof(description), ItemImageConsts.DescriptionMaxLength);
            Check.NotNullOrWhiteSpace(url, nameof(url));
            Check.Length(url, nameof(url), ItemImageConsts.UrlMaxLength, ItemImageConsts.UrlMinLength);

            var queryable = await _itemImageRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var itemImage = await AsyncExecuter.FirstOrDefaultAsync(query);

            itemImage.ItemId = itemId;
            itemImage.Description = description;
            itemImage.Url = url;
            itemImage.Active = active;
            itemImage.DisplayOrder = displayOrder;

            itemImage.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemImageRepository.UpdateAsync(itemImage);
        }

    }
}
using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Items
{
    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemsAppService
    {
        public async Task<ItemProfileDto> GetItemProfileAsync(Guid id)
        {
            Item item = await _itemRepository.GetAsync(id);
            List<ItemAttachment> attachments = (await _itemAttachmentRepository.GetQueryableAsync()).Where(x => x.ItemId == id).ToList();
            List<ItemImage> images = (await _itemImageRepository.GetQueryableAsync()).Where(x => x.ItemId == id).ToList();
            var result = new ItemProfileDto()
            {
                Item = ObjectMapper.Map<Item, ItemDto>(item),
                Attachments = ObjectMapper.Map<List<ItemAttachment>, List<ItemAttachmentDto>>(attachments),
                Images = ObjectMapper.Map<List<ItemImage>, List<ItemImageDto>>(images),
            };
            return result;
        }
    }
}

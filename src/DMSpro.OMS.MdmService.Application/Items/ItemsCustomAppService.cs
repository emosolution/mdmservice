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

        public async Task<string> GetItemInfoForSOAsync(Guid companyId, DateTime lastApiDate)
        {
            Dictionary<string, List<string>> test1 = new()
            {
                {"1", new List<string>(){ "111", "111111" }},
                {"2", new List<string>(){ "2222", "23333333" }},
            };
            Dictionary<string, ItemDto> test2 = new()
            {
                {"222", new ItemDto() },
                {"333", new ItemDto() },
            };
            string jsonResult = $"{{routeWithItemList:{_jsonSerializer.Serialize(test1)}, " +
                $"itemFullDetails:{_jsonSerializer.Serialize(test2)}}}";
            return jsonResult;
        }

        /*
        public async Task<ItemProfileWithDataDto> GetItemProfileWithData(Guid id)
        {
            Item item = await _itemRepository.GetAsync(id);
            List<ItemAttachment> attachments = (await _itemAttachmentRepository.GetQueryableAsync()).Where(x => x.ItemId == id).ToList();
            List<ItemImage> images = (await _itemImageRepository.GetQueryableAsync()).Where(x => x.ItemId == id).ToList();
            List<ItemAttachmentCreateDto> attachmentsWithData = new();
            List<ItemImageCreateDto> imagesWithData = new();

            foreach (ItemAttachment attachment in attachments)
            {
                ItemAttachmentCreateDto dto = new()
                {
                    Description = attachment.Description,
                    Active = attachment.Active,
                    ItemId = attachment.ItemId,
                    File = await _itemAttachmentsAppService.GetFile(attachment.FileId),
                };
                attachmentsWithData.Add(dto);
            }

            foreach (ItemImage image in images)
            {
                ItemImageCreateDto dto = new()
                {
                    Description = image.Description,
                    Active = image.Active,
                    ItemId = image.ItemId,
                    DisplayOrder = image.DisplayOrder,
                    File = await _itemImagesAppService.GetFile(image.FileId),
                };
                imagesWithData.Add(dto);
            }

            var result = new ItemProfileWithDataDto()
            {
                Item = ObjectMapper.Map<Item, ItemDto>(item),
                Attachments = attachmentsWithData,
                Images = imagesWithData,
            };
            return result;
        }
        */
    }
}

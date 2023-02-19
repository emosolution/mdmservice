using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public partial class ItemAttachmentsAppService
    {
        [Authorize(MdmServicePermissions.Items.Create)]
        public virtual async Task<ItemAttachmentDto> CreateAsync(ItemAttachmentCreateDto input)
        {
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }
            var itemAttachment = await _itemAttachmentManager.CreateAsync(
            input.ItemId, input.Description, input.Url, input.Active, input.FileId
            );

            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(itemAttachment);
        }

        [Authorize(MdmServicePermissions.Items.Edit)]
        public virtual async Task<ItemAttachmentDto> UpdateAsync(Guid id, ItemAttachmentUpdateDto input)
        {
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }

            var itemAttachment = await _itemAttachmentManager.UpdateAsync(
            id,
            input.ItemId, input.Description, input.Url, input.Active, input.FileId, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(itemAttachment);
        }

        /*
        [Authorize(MdmServicePermissions.Items.Create)]
        public virtual async Task<ItemAttachmentDto> CreateAsync(ItemAttachmentCreateDto input)
        {
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }
            //if (input.File == null)
            //{
            //    throw new UserFriendlyException(L["The {0} field is required.", L["File"]]);
            //}
            //Guid directoryId = _fileManagementInfoAppService.ItemAttachmentDirectoryId;
            //using (var stream = new MemoryStream()) {
            //    await input.File.GetStream().CopyToAsync(stream);

            //    string url = "";

            //    var itemAttachment = await _itemAttachmentManager.CreateAsync(
            //    input.ItemId, input.Description, url, input.Active, input.FileId
            //    );
            //}

            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(itemAttachment);
        }

        [Authorize(MdmServicePermissions.Items.Edit)]
        public virtual async Task<ItemAttachmentDto> UpdateAsync(Guid id, ItemAttachmentUpdateDto input)
        {
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }

            string url = "";

            var itemAttachment = await _itemAttachmentManager.UpdateAsync(
            id,
            input.ItemId, input.Description, url, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(itemAttachment);
        }
        */
    }
}

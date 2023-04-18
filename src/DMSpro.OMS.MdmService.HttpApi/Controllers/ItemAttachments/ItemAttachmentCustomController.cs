using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.ItemAttachments
{
    public partial class ItemAttachmentController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual Task DeleteManyAsync(List<Guid> ids)
        {
            return _itemAttachmentsAppService.DeleteManyAsync(ids);
        }

        [HttpGet]
        [Route("get-file")]
        public virtual Task<IRemoteStreamContent> GetFileAsync(Guid id)
        {
            return _itemAttachmentsAppService.GetFileAsync(id);
        }


        [HttpPost]
        public virtual Task<ItemAttachmentDto> CreateAsync([Required] Guid itemId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(ItemImageConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            return _itemAttachmentsAppService.CreateAsync(itemId, inputFile,
                    description, active);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemAttachmentDto> UpdateAsync(Guid id, [Required] Guid itemId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(ItemImageConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            return _itemAttachmentsAppService.UpdateAsync(id, itemId, inputFile,
                    description, active);
        }
    }
}

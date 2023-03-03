using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.ItemAttachments
{
    public partial class ItemAttachmentController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            try
            {
                await _itemAttachmentsAppService.DeleteManyAsync(ids);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }

        [HttpGet]
        [Route("get-file")]
        public virtual async Task<IRemoteStreamContent> GetFileAsync(Guid id)
        {
            try
            {
                return await _itemAttachmentsAppService.GetFileAsync(id);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }


        [HttpPost]
        public virtual async Task<ItemAttachmentDto> CreateAsync([Required] Guid itemId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(ItemImageConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            try
            {
                return await _itemAttachmentsAppService.CreateAsync(itemId, inputFile,
                    description, active);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<ItemAttachmentDto> UpdateAsync(Guid id, [Required] Guid itemId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(ItemImageConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            try
            {
                return await _itemAttachmentsAppService.UpdateAsync(id, itemId, inputFile,
                    description, active);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }
    }
}

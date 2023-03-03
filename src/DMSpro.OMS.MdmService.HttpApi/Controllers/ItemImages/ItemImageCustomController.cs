using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.ItemImages
{
    public partial class ItemImageController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            try
            {
                await _itemImagesAppService.DeleteManyAsync(ids);
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
                return await _itemImagesAppService.GetFileAsync(id);
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
        public virtual async Task<ItemImageDto> CreateAsync([Required] Guid itemId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(ItemImageConsts.DescriptionMaxLength)] string description,
            bool active = true, int displayOrder = 0)
        {
            try
            {
                return await _itemImagesAppService.CreateAsync(itemId, inputFile,
                    description, active, displayOrder);
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
        public virtual async Task<ItemImageDto> UpdateAsync(Guid id, [Required] Guid itemId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(ItemImageConsts.DescriptionMaxLength)] string description,
            bool active = true, int displayOrder = 0)
        {
            try
            {
                return await _itemImagesAppService.UpdateAsync(id, itemId, inputFile,
                    description, active, displayOrder);
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

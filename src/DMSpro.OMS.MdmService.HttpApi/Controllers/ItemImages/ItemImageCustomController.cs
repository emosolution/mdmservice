using DMSpro.OMS.MdmService.ItemImages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.ItemImages
{
    public partial class ItemImageController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual Task DeleteManyAsync(List<Guid> ids)
        {
            return _itemImagesAppService.DeleteManyAsync(ids);
        }

        [HttpGet]
        [Route("get-file")]
        public virtual Task<IRemoteStreamContent> GetFileAsync(Guid id)
        {
            return _itemImagesAppService.GetFileAsync(id);
        }

        [HttpPost]
        public virtual Task<ItemImageDto> CreateAsync([Required] Guid itemId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(ItemImageConsts.DescriptionMaxLength)] string description,
            bool active = true, int displayOrder = 0)
        {
            return _itemImagesAppService.CreateAsync(itemId, inputFile,
                    description, active, displayOrder);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemImageDto> UpdateAsync(Guid id, [Required] Guid itemId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(ItemImageConsts.DescriptionMaxLength)] string description,
            bool active = true, int displayOrder = 0)
        {
            return _itemImagesAppService.UpdateAsync(id, itemId, inputFile,
                    description, active, displayOrder);
        }
    }
}

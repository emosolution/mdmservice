using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public virtual Task<IRemoteStreamContent> GetFile(Guid id)
        {
            return _itemImagesAppService.GetFile(id);
        }
    }
}

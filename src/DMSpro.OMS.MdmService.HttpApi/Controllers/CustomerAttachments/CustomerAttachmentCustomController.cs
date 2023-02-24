using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.CustomerAttachments
{
    public partial class CustomerAttachmentController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual Task DeleteManyAsync(List<Guid> ids)
        {
            return _customerAttachmentsAppService.DeleteManyAsync(ids);
        }

        [HttpGet]
        [Route("get-file")]
        public virtual Task<IRemoteStreamContent> GetFileAsync(Guid id)
        {
            return _customerAttachmentsAppService.GetFileAsync(id);
        }
    }
}

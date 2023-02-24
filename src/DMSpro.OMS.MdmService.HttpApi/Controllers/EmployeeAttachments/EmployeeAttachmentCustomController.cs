using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeAttachments
{
    public partial class EmployeeAttachmentController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual Task DeleteManyAsync(List<Guid> ids)
        {
            return _employeeAttachmentsAppService.DeleteManyAsync(ids);
        }

        [HttpGet]
        [Route("get-file")]
        public virtual Task<IRemoteStreamContent> GetFileAsync(Guid id)
        {
            return _employeeAttachmentsAppService.GetFileAsync(id);
        }
    }
}

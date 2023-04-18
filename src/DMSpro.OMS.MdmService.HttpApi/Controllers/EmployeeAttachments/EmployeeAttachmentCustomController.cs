using DMSpro.OMS.MdmService.EmployeeAttachments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        [HttpPost]
        public virtual Task<EmployeeAttachmentDto> CreateAsync([Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            return _employeeAttachmentsAppService.CreateAsync(employeeId,
                    inputFile, description, active);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeAttachmentDto> UpdateAsync(Guid id, [Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            return _employeeAttachmentsAppService.UpdateAsync(id, employeeId,
                    inputFile, description, active);
        }
    }
}

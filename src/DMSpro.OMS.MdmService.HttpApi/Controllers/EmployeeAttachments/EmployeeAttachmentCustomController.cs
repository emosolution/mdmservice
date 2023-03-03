using DMSpro.OMS.MdmService.EmployeeAttachments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeAttachments
{
    public partial class EmployeeAttachmentController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            try
            {
                await _employeeAttachmentsAppService.DeleteManyAsync(ids);
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
                return await _employeeAttachmentsAppService.GetFileAsync(id);
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
        public virtual async Task<EmployeeAttachmentDto> CreateAsync([Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            try
            {
                return await _employeeAttachmentsAppService.CreateAsync(employeeId,
                    inputFile, description, active);
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
        public virtual async Task<EmployeeAttachmentDto> UpdateAsync(Guid id, [Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            try
            {
                return await _employeeAttachmentsAppService.UpdateAsync(id, employeeId,
                    inputFile, description, active);
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

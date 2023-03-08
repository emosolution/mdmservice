using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeImages
{
    public partial class EmployeeImageController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            try
            {
                await _employeeImagesAppService.DeleteManyAsync(ids);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpGet]
        [Route("get-file")]
        public virtual async Task<IRemoteStreamContent> GetFileAsync(Guid id)
        {
            try
            {
                return await _employeeImagesAppService.GetFileAsync(id);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPost]
        public virtual async Task<EmployeeImageDto> CreateAsync([Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            try
            {
                return await _employeeImagesAppService.CreateAsync(employeeId, inputFile, 
                    description, active);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<EmployeeImageDto> UpdateAsync(Guid id, [Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            try
            {
                return await _employeeImagesAppService.UpdateAsync(id, employeeId, inputFile, 
                    description, active);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPost]
        [Route("avatar")]
        public virtual async Task<EmployeeImageDto> CreateAvatarAsync([Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            try
            {
                return await _employeeImagesAppService.CreateAvatarAsync(employeeId, inputFile,
                    description, active);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPut]
        [Route("avatar")]
        public virtual async Task<EmployeeImageDto> UpdateAvatarAsync([Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            try
            {
                return await _employeeImagesAppService.UpdateAvatarAsync(employeeId, inputFile,
                    description, active);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }
    }
}

using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeImages
{
    public partial class EmployeeImageController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual Task DeleteManyAsync(List<Guid> ids)
        {
            return _employeeImagesAppService.DeleteManyAsync(ids);
        }

        [HttpGet]
        [Route("get-file")]
        public virtual Task<IRemoteStreamContent> GetFileAsync(Guid id)
        {
            return _employeeImagesAppService.GetFileAsync(id);
        }

        [HttpPost]
        public virtual Task<EmployeeImageDto> CreateAsync([Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            return _employeeImagesAppService.CreateAsync(employeeId, inputFile,
                    description, active);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeImageDto> UpdateAsync(Guid id, [Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            return _employeeImagesAppService.UpdateAsync(id, employeeId, inputFile,
                    description, active);
        }

        [HttpPost]
        [Route("avatar")]
        public virtual Task<EmployeeImageDto> CreateAvatarAsync([Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            return _employeeImagesAppService.CreateAvatarAsync(employeeId, inputFile,
                    description, active);
        }

        [HttpPut]
        [Route("avatar")]
        public virtual Task<EmployeeImageDto> UpdateAvatarAsync([Required] Guid employeeId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            return _employeeImagesAppService.UpdateAvatarAsync(employeeId, inputFile,
                    description, active);
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.EmployeeAttachments;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DMSpro.OMS.MdmService.CustomerImages;

namespace DMSpro.OMS.MdmService.Controllers.CustomerImages
{
    public partial class CustomerImageController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual Task DeleteManyAsync(List<Guid> ids)
        {
            return _customerImagesAppService.DeleteManyAsync(ids);
        }

        [HttpGet]
        [Route("get-file")]
        public virtual Task<IRemoteStreamContent> GetFileAsync(Guid id)
        {
            return _customerImagesAppService.GetFileAsync(id);
        }

        [HttpPost]
        public virtual Task<CustomerImageDto> CreateAsync([Required] Guid customerId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(CustomerImageConsts.DescriptionMaxLength)] string description,
            bool active = true, bool isPOSM = false, Guid? itemPOSMId = null)
        {
            return _customerImagesAppService.CreateAsync(customerId, inputFile,
                    description, active, isPOSM, itemPOSMId);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerImageDto> UpdateAsync(Guid id, 
            [Required] Guid customerId, 
            [Required] IRemoteStreamContent inputFile,
            [StringLength(CustomerImageConsts.DescriptionMaxLength)] string description, 
            bool active = true, bool isPOSM = false, Guid? itemPOSMId = null)
        {
            return _customerImagesAppService.UpdateAsync(id, customerId, inputFile,
                    description, active, isPOSM, itemPOSMId);
        }

        [HttpPost]
        [Route("avatar")]
        public virtual Task<CustomerImageDto> CreateAvatarAsync([Required] Guid customerId, 
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description)
        {
            return _customerImagesAppService.CreateAvatarAsync(customerId, 
                    inputFile, description);
        }

        [HttpPut]
        [Route("avatar")]
        public virtual Task<CustomerImageDto> UpdateAvatarAsync([Required] Guid customerId, 
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description)
        {
            return _customerImagesAppService.UpdateAvatarAsync(customerId, 
                    inputFile, description);
        }
    }
}
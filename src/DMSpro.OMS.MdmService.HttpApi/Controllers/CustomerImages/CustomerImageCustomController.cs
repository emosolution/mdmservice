using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using DMSpro.OMS.MdmService.CustomerImages;

namespace DMSpro.OMS.MdmService.Controllers.CustomerImages
{
    public partial class CustomerImageController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            try
            {
                await _customerImagesAppService.DeleteManyAsync(ids);
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
                return await _customerImagesAppService.GetFileAsync(id);
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
        public virtual async Task<CustomerImageDto> CreateAsync([Required] Guid customerId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(CustomerImageConsts.DescriptionMaxLength)] string description,
            bool active = true, bool isPOSM = false, Guid? itemPOSMId = null)
        {
            try
            {
                return await _customerImagesAppService.CreateAsync(customerId, inputFile,
                    description, active, isPOSM, itemPOSMId);
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
        public virtual async Task<CustomerImageDto> UpdateAsync(Guid id, 
            [Required] Guid customerId, 
            [Required] IRemoteStreamContent inputFile,
            [StringLength(CustomerImageConsts.DescriptionMaxLength)] string description, 
            bool active = true, bool isPOSM = false, Guid? itemPOSMId = null)
        {
            try
            {
                return await _customerImagesAppService.UpdateAsync(id, customerId, inputFile,
                    description, active, isPOSM, itemPOSMId);
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
        [Route("avatar")]
        public virtual async Task<CustomerImageDto> CreateAvatarAsync([Required] Guid customerId, 
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description)
        {
            try
            {
                return await _customerImagesAppService.CreateAvatarAsync(customerId, 
                    inputFile, description);
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
        [Route("avatar")]
        public virtual async Task<CustomerImageDto> UpdateAvatarAsync([Required] Guid customerId, 
            [Required] IRemoteStreamContent inputFile,
            [StringLength(EmployeeAttachmentConsts.DescriptionMaxLength)] string description)
        {
            try
            {
                return await _customerImagesAppService.UpdateAvatarAsync(customerId, 
                    inputFile, description);
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
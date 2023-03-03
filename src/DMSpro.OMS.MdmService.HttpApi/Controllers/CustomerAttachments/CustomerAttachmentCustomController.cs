using DMSpro.OMS.MdmService.CustomerAttachments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.CustomerAttachments
{
    public partial class CustomerAttachmentController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            try
            {
                await _customerAttachmentsAppService.DeleteManyAsync(ids);
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
                return await _customerAttachmentsAppService.GetFileAsync(id);
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
        public virtual async Task<CustomerAttachmentDto> CreateAsync([Required] Guid customerId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(CustomerAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            try
            {
                return await _customerAttachmentsAppService.CreateAsync(customerId, inputFile,
                    description, active);
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
        public virtual async Task<CustomerAttachmentDto> UpdateAsync(Guid id, [Required] Guid customerId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(CustomerAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            try
            {
                return await _customerAttachmentsAppService.UpdateAsync(id, customerId, inputFile,
                    description, active);
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

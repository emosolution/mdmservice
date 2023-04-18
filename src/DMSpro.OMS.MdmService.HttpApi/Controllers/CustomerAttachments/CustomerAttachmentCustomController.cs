using DMSpro.OMS.MdmService.CustomerAttachments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [HttpPost]
        public virtual Task<CustomerAttachmentDto> CreateAsync([Required] Guid customerId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(CustomerAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            return _customerAttachmentsAppService.CreateAsync(customerId, inputFile,
                    description, active);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerAttachmentDto> UpdateAsync(Guid id, [Required] Guid customerId,
            [Required] IRemoteStreamContent inputFile,
            [StringLength(CustomerAttachmentConsts.DescriptionMaxLength)] string description,
            bool active = true)
        {
            return _customerAttachmentsAppService.UpdateAsync(id, customerId, inputFile,
                description, active);
        }
    }
}

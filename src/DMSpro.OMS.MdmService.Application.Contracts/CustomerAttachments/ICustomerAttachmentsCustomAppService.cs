using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Content;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public partial interface ICustomerAttachmentsAppService
    {
        Task DeleteManyAsync(List<Guid> ids);

        Task<IRemoteStreamContent> GetFileAsync(Guid id);

        Task<CustomerAttachmentDto> CreateAsync(Guid customerId,
            IRemoteStreamContent inputFile,
            string description, bool active);

        Task<CustomerAttachmentDto> UpdateAsync(Guid id, Guid customerId,
            IRemoteStreamContent inputFile,
            string description, bool active);

    }
}

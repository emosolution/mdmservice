using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public partial interface ICustomerAttachmentsAppService
    {
        Task DeleteManyAsync(List<Guid> id);

        Task<IRemoteStreamContent> GetFile(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public partial interface IItemAttachmentsAppService
    {
        Task DeleteManyAsync(List<Guid> id);

    }
}

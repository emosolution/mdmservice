using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public partial interface IItemAttachmentsAppService
    {
        Task DeleteManyAsync(List<Guid> id);

        Task<IRemoteStreamContent> GetFileAsync(Guid id);

        Task<ItemAttachmentDto> CreateAsync(Guid itemId,
            IRemoteStreamContent inputFile,
            string description, bool active);

        Task<ItemAttachmentDto> UpdateAsync(Guid id, Guid itemId,
            IRemoteStreamContent inputFile,
            string description, bool active);
    }
}

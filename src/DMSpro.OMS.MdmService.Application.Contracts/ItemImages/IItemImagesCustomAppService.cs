using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public partial interface IItemImagesAppService
    {
        Task DeleteManyAsync(List<Guid> id);

        Task<IRemoteStreamContent> GetFile(Guid id);
    }
}

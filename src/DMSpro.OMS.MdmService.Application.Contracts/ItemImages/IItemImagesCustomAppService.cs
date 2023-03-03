using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public partial interface IItemImagesAppService
    {
        Task DeleteManyAsync(List<Guid> id);

        Task<IRemoteStreamContent> GetFileAsync(Guid id);

        Task<ItemImageDto> CreateAsync(Guid itemId,
            IRemoteStreamContent inputFile,
            string description, bool active, int displayOrder);

        Task<ItemImageDto> UpdateAsync(Guid id, Guid itemId,
            IRemoteStreamContent inputFile,
            string description, bool active, int displayOrder);

    }
}

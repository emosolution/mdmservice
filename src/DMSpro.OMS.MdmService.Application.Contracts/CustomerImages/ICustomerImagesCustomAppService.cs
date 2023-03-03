using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public partial interface ICustomerImagesAppService
    {
        Task DeleteManyAsync(List<Guid> id);

        Task<IRemoteStreamContent> GetFileAsync(Guid id);

        Task<CustomerImageDto> CreateAsync(Guid customerId,
            IRemoteStreamContent inputFile,
            string description, bool active, bool isPOSM,
            Guid? itemPOSMId);

        Task<CustomerImageDto> UpdateAsync(Guid id, Guid customerId,
            IRemoteStreamContent inputFile,
            string description, bool active, bool isPOSM,
            Guid? itemPOSMId);

        Task<CustomerImageDto> CreateAvatarAsync(Guid customerId,
            IRemoteStreamContent inputFile, string description);

        Task<CustomerImageDto> UpdateAvatarAsync(Guid customerId,
            IRemoteStreamContent inputFile, string description);

    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public partial interface IEmployeeImagesAppService
    {
        Task DeleteManyAsync(List<Guid> id);

        Task<IRemoteStreamContent> GetFileAsync(Guid id);

        Task<EmployeeImageDto> CreateAvatarAsync(EmployeeImageCreateDto input, IRemoteStreamContent file);

        Task<EmployeeImageDto> UpdateAvatarAsync(EmployeeImageUpdateDto input, IRemoteStreamContent file);

        Task<EmployeeImageDto> TestCreateAvatarAsync(Guid id, IRemoteStreamContent file);

        Task<EmployeeImageDto> TestCreateAvatarOnlyFileAsync(IRemoteStreamContent file);


        Task<EmployeeImageDto> TestCreateAvatarExtensibleAsync(AvartarCreateDto input);
    }
}

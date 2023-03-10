using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public partial interface IEmployeeImagesAppService
    {
        Task DeleteManyAsync(List<Guid> ids);

        Task<IRemoteStreamContent> GetFileAsync(Guid id);

        Task<EmployeeImageDto> CreateAsync(Guid employeeId,
            IRemoteStreamContent inputFile,
            string description, bool active);

        Task<EmployeeImageDto> UpdateAsync(Guid id, Guid employeeId,
            IRemoteStreamContent inputFile,
            string description, bool active);

        Task<EmployeeImageDto> CreateAvatarAsync(Guid employeeId,
            IRemoteStreamContent inputFile,
            string description, bool active);

        Task<EmployeeImageDto> UpdateAvatarAsync(Guid employeeId,
            IRemoteStreamContent inputFile,
            string description, bool active);
    }
}

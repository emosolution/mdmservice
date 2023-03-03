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

        Task<EmployeeImageDto> CreateAsync(Guid employeeId, string description, 
            bool active, IRemoteStreamContent inputFile);

        Task<EmployeeImageDto> UpdateAsync(Guid id, Guid employeeId, string description, 
            bool active, IRemoteStreamContent inputFile);

        Task<EmployeeImageDto> CreateAvatarAsync(Guid employeeId, string description,
            bool active, IRemoteStreamContent inputFile);

        Task<EmployeeImageDto> UpdateAvatarAsync(Guid employeeId, string description, 
            bool active, IRemoteStreamContent inputFile);
    }
}

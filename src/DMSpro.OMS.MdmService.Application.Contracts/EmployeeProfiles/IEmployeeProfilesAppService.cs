using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public partial interface IEmployeeProfilesAppService : IApplicationService
    {
        Task<EmployeeProfileDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<EmployeeProfileDto> CreateAsync(EmployeeProfileCreateDto input);

        Task<EmployeeProfileDto> UpdateAsync(Guid id, EmployeeProfileUpdateDto input);
    }
}
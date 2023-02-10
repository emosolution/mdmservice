using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public partial interface IEmployeeProfilesAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<EmployeeProfileWithNavigationPropertiesDto>> GetListAsync(GetEmployeeProfilesInput input);

        Task<EmployeeProfileWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<EmployeeProfileDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetWorkingPositionLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetSystemDataLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<EmployeeProfileDto> CreateAsync(EmployeeProfileCreateDto input);

        Task<EmployeeProfileDto> UpdateAsync(Guid id, EmployeeProfileUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeProfileExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public interface IEmployeeProfilesAppService : IApplicationService
    {
        Task<PagedResultDto<EmployeeProfileWithNavigationPropertiesDto>> GetListAsync(GetEmployeeProfilesInput input);

        Task<EmployeeProfileWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

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
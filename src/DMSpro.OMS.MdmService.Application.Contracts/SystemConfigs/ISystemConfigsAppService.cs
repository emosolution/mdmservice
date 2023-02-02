using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public partial interface ISystemConfigsAppService : IApplicationService
    {
        Task<PagedResultDto<SystemConfigDto>> GetListAsync(GetSystemConfigsInput input);

        Task<SystemConfigDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SystemConfigDto> CreateAsync(SystemConfigCreateDto input);

        Task<SystemConfigDto> UpdateAsync(Guid id, SystemConfigUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SystemConfigExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
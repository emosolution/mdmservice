using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public partial interface IWorkingPositionsAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<WorkingPositionDto>> GetListAsync(GetWorkingPositionsInput input);

        Task<WorkingPositionDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<WorkingPositionDto> CreateAsync(WorkingPositionCreateDto input);

        Task<WorkingPositionDto> UpdateAsync(Guid id, WorkingPositionUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(WorkingPositionExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
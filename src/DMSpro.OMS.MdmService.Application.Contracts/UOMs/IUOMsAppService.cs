using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.UOMs
{
    public partial interface IUOMsAppService : IApplicationService
    {
        Task<PagedResultDto<UOMDto>> GetListAsync(GetUOMsInput input);

        Task<UOMDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<UOMDto> CreateAsync(UOMCreateDto input);

        Task<UOMDto> UpdateAsync(Guid id, UOMUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.Streets
{
    public partial interface IStreetsAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<StreetDto>> GetListAsync(GetStreetsInput input);

        Task<StreetDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<StreetDto> CreateAsync(StreetCreateDto input);

        Task<StreetDto> UpdateAsync(Guid id, StreetUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(StreetExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
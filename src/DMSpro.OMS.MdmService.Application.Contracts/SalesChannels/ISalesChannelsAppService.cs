using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.SalesChannels
{
    public partial interface ISalesChannelsAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<SalesChannelDto>> GetListAsync(GetSalesChannelsInput input);
        
        Task<SalesChannelDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SalesChannelDto> CreateAsync(SalesChannelCreateDto input);

        Task<SalesChannelDto> UpdateAsync(Guid id, SalesChannelUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesChannelExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
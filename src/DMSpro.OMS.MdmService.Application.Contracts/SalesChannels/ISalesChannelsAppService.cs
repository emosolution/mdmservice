using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.SalesChannels
{
    public interface ISalesChannelsAppService : IApplicationService
    {
        Task<PagedResultDto<SalesChannelDto>> GetListAsync(GetSalesChannelsInput input);
        
        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);
        
        Task<SalesChannelDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SalesChannelDto> CreateAsync(SalesChannelCreateDto input);

        Task<SalesChannelDto> UpdateAsync(Guid id, SalesChannelUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesChannelExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public partial interface IPriceUpdatesAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<PriceUpdateWithNavigationPropertiesDto>> GetListAsync(GetPriceUpdatesInput input);

        Task<PriceUpdateWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<PriceUpdateDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<PriceUpdateDto> CreateAsync(PriceUpdateCreateDto input);

        Task<PriceUpdateDto> UpdateAsync(Guid id, PriceUpdateUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceUpdateExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
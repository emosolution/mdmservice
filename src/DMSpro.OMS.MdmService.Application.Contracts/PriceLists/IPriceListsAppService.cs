using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;


namespace DMSpro.OMS.MdmService.PriceLists
{
    public partial interface IPriceListsAppService : IApplicationService
    {
        Task<PagedResultDto<PriceListWithNavigationPropertiesDto>> GetListAsync(GetPriceListsInput input);

        Task<PriceListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<PriceListDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetPriceListLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<PriceListDto> CreateAsync(PriceListCreateDto input);

        Task<PriceListDto> UpdateAsync(Guid id, PriceListUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceListExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
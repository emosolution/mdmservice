using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;


namespace DMSpro.OMS.MdmService.Routes
{
    public partial interface IRoutesAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<RouteWithNavigationPropertiesDto>> GetListAsync(GetRoutesInput input);

        Task<RouteWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<RouteDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<RouteDto> CreateAsync(RouteCreateDto input);

        Task<RouteDto> UpdateAsync(Guid id, RouteUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(RouteExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
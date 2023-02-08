using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;


namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public partial interface ISalesOrgHeadersAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<SalesOrgHeaderDto>> GetListAsync(GetSalesOrgHeadersInput input);

        Task<SalesOrgHeaderDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SalesOrgHeaderDto> CreateAsync(SalesOrgHeaderCreateDto input);

        Task<SalesOrgHeaderDto> UpdateAsync(Guid id, SalesOrgHeaderUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgHeaderExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
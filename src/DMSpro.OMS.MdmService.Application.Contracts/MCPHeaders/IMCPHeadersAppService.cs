using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;


namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public partial interface IMCPHeadersAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<MCPHeaderWithNavigationPropertiesDto>> GetListAsync(GetMCPHeadersInput input);

        Task<MCPHeaderWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<MCPHeaderDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetItemGroupLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<MCPHeaderDto> CreateAsync(MCPHeaderCreateDto input);

        Task<MCPHeaderDto> UpdateAsync(Guid id, MCPHeaderUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(MCPHeaderExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public partial interface IMCPDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<MCPDetailWithNavigationPropertiesDto>> GetListAsync(GetMCPDetailsInput input);

        Task<MCPDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
        
        Task<MCPDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetMCPHeaderLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<MCPDetailDto> CreateAsync(MCPDetailCreateDto input);

        Task<MCPDetailDto> UpdateAsync(Guid id, MCPDetailUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(MCPDetailExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
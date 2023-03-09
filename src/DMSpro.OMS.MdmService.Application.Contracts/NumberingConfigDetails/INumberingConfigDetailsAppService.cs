using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial interface INumberingConfigDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<NumberingConfigDetailWithNavigationPropertiesDto>> GetListAsync(GetNumberingConfigDetailsInput input);

        Task<NumberingConfigDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<NumberingConfigDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetNumberingConfigLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<NumberingConfigDetailDto> CreateAsync(NumberingConfigDetailCreateDto input);

        Task<NumberingConfigDetailDto> UpdateAsync(Guid id, NumberingConfigDetailUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(NumberingConfigDetailExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
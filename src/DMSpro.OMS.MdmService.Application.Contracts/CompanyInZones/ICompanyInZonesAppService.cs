using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public partial interface ICompanyInZonesAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<CompanyInZoneWithNavigationPropertiesDto>> GetListAsync(GetCompanyInZonesInput input);

        Task<CompanyInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
        
        Task<CompanyInZoneDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CompanyInZoneDto> CreateAsync(CompanyInZoneCreateDto input);

        Task<CompanyInZoneDto> UpdateAsync(Guid id, CompanyInZoneUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyInZoneExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
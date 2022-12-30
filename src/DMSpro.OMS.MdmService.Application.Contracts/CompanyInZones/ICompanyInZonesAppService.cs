using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public interface ICompanyInZonesAppService : IApplicationService
    {
        Task<PagedResultDto<CompanyInZoneWithNavigationPropertiesDto>> GetListAsync(GetCompanyInZonesInput input);

        Task<CompanyInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

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
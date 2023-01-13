using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public interface IEmployeeInZonesAppService : IApplicationService
    {
        Task<PagedResultDto<EmployeeInZoneWithNavigationPropertiesDto>> GetListAsync(GetEmployeeInZonesInput input);

        Task<EmployeeInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<EmployeeInZoneDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<EmployeeInZoneDto> CreateAsync(EmployeeInZoneCreateDto input);

        Task<EmployeeInZoneDto> UpdateAsync(Guid id, EmployeeInZoneUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeInZoneExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
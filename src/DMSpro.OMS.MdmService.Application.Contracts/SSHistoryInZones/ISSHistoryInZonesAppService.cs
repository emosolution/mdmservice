using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.SSHistoryInZones
{
    public interface ISSHistoryInZonesAppService : IApplicationService
    {
        Task<PagedResultDto<SSHistoryInZoneWithNavigationPropertiesDto>> GetListAsync(GetSSHistoryInZonesInput input);

        Task<SSHistoryInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<SSHistoryInZoneDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<SSHistoryInZoneDto> CreateAsync(SSHistoryInZoneCreateDto input);

        Task<SSHistoryInZoneDto> UpdateAsync(Guid id, SSHistoryInZoneUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SSHistoryInZoneExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
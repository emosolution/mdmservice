using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public interface ICustomerInZonesAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerInZoneWithNavigationPropertiesDto>> GetListAsync(GetCustomerInZonesInput input);

        Task<CustomerInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<CustomerInZoneDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerInZoneDto> CreateAsync(CustomerInZoneCreateDto input);

        Task<CustomerInZoneDto> UpdateAsync(Guid id, CustomerInZoneUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerInZoneExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
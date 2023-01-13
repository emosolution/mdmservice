using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public interface ICustomerGroupByGeosAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerGroupByGeoWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByGeosInput input);

        Task<CustomerGroupByGeoWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<CustomerGroupByGeoDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetGeoMasterLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerGroupByGeoDto> CreateAsync(CustomerGroupByGeoCreateDto input);

        Task<CustomerGroupByGeoDto> UpdateAsync(Guid id, CustomerGroupByGeoUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByGeoExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
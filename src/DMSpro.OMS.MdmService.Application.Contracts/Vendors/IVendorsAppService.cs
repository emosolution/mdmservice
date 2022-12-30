using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.Vendors
{
    public interface IVendorsAppService : IApplicationService
    {
        Task<PagedResultDto<VendorWithNavigationPropertiesDto>> GetListAsync(GetVendorsInput input);

        Task<VendorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<VendorDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetGeoMasterLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<VendorDto> CreateAsync(VendorCreateDto input);

        Task<VendorDto> UpdateAsync(Guid id, VendorUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Partial;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public partial interface IGeoMastersAppService : IApplicationService
    {
        Task<PagedResultDto<GeoMasterWithNavigationPropertiesDto>> GetListAsync(GetGeoMastersInput input);
        
        Task<GeoMasterWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<GeoMasterDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetGeoMasterLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<GeoMasterDto> CreateAsync(GeoMasterCreateDto input);

        Task<GeoMasterDto> UpdateAsync(Guid id, GeoMasterUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(GeoMasterExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);
        Task<int> InsertFromExcelAsync(IFormFile file);
        Task<int> UpdateFromExcelAsync(IFormFile file);
    }
}
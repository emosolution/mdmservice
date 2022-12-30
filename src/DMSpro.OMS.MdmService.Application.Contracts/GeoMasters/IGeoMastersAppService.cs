using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public interface IGeoMastersAppService : IApplicationService
    {
        Task<PagedResultDto<GeoMasterWithNavigationPropertiesDto>> GetListAsync(GetGeoMastersInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);
        Task<GeoMasterWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<GeoMasterDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetGeoMasterLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<GeoMasterDto> CreateAsync(GeoMasterCreateDto input);

        Task<GeoMasterDto> UpdateAsync(Guid id, GeoMasterUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(GeoMasterExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
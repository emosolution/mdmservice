using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public interface IHolidayDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<HolidayDetailWithNavigationPropertiesDto>> GetListAsync(GetHolidayDetailsInput input);

        Task<HolidayDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<HolidayDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetHolidayLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<HolidayDetailDto> CreateAsync(HolidayDetailCreateDto input);

        Task<HolidayDetailDto> UpdateAsync(Guid id, HolidayDetailUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(HolidayDetailExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
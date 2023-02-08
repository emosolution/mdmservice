using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public partial interface IHolidayDetailsAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<HolidayDetailWithNavigationPropertiesDto>> GetListAsync(GetHolidayDetailsInput input);

        Task<HolidayDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
        
        Task<HolidayDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetHolidayLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<HolidayDetailDto> CreateAsync(HolidayDetailCreateDto input);

        Task<HolidayDetailDto> UpdateAsync(Guid id, HolidayDetailUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(HolidayDetailExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
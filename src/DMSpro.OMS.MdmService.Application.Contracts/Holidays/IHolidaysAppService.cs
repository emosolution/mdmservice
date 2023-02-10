using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.Holidays
{
    public partial interface IHolidaysAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<HolidayDto>> GetListAsync(GetHolidaysInput input);

        Task<HolidayDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<HolidayDto> CreateAsync(HolidayCreateDto input);

        Task<HolidayDto> UpdateAsync(Guid id, HolidayUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(HolidayExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
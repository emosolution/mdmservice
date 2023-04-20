using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Holidays
{
    public partial interface IHolidaysAppService : IApplicationService
    {
        Task<HolidayDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<HolidayDto> CreateAsync(HolidayCreateDto input);

        Task<HolidayDto> UpdateAsync(Guid id, HolidayUpdateDto input);
    }
}
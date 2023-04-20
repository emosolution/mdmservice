using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public partial interface IHolidayDetailsAppService : IApplicationService
    {
        Task<HolidayDetailDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<HolidayDetailDto> CreateAsync(HolidayDetailCreateDto input);

        Task<HolidayDetailDto> UpdateAsync(Guid id, HolidayDetailUpdateDto input);
    }
}
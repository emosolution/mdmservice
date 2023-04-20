using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using System.Linq;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.Holidays
{

    [Authorize(MdmServicePermissions.Holidays.Default)]
    public partial class HolidaysAppService
    {
        public virtual async Task<HolidayDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Holiday, HolidayDto>(await _holidayRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.Holidays.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            if (await _holidayDetailRepository.AnyAsync(x => x.HolidayId == id))
            {
                throw new UserFriendlyException(message: L["Error:HolidaysAppService:550"], code: "0");
            }
            await _holidayRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Holidays.Create)]
        public virtual async Task<HolidayDto> CreateAsync(HolidayCreateDto input)
        {
            Check.Range(input.Year, nameof(input.Year), HolidayConsts.YearMinLength, HolidayConsts.YearMaxLength);
            Check.Length(input.Description, nameof(input.Description), HolidayConsts.DescriptionMaxLength);
            if (await _holidayRepository.AnyAsync(x => x.Year == input.Year))
            {
                throw new UserFriendlyException(message: L["Error:HolidaysAppService:552"], code: "0");
            }
            var holiday = new Holiday(GuidGenerator.Create(), input.Year, input.Description);
            await _holidayRepository.InsertAsync(holiday);
            return ObjectMapper.Map<Holiday, HolidayDto>(holiday);
        }

        [Authorize(MdmServicePermissions.Holidays.Edit)]
        public virtual async Task<HolidayDto> UpdateAsync(Guid id, HolidayUpdateDto input)
        {
            Check.Range(input.Year, nameof(input.Year), HolidayConsts.YearMinLength, HolidayConsts.YearMaxLength);
            Check.Length(input.Description, nameof(input.Description), HolidayConsts.DescriptionMaxLength);

            var holiday = await _holidayRepository.GetAsync(id);
            if (await _holidayRepository.AnyAsync(x => x.Year == input.Year && x.Id != id))
            {
                throw new UserFriendlyException(message: L["Error:HolidaysAppService:552"], code: "0");
            }
            var details = await _holidayDetailRepository.GetListAsync(x => x.HolidayId == id);
            if (details.Any() && holiday.Year != input.Year)
            {
                throw new UserFriendlyException(message: L["Error:HolidaysAppService:551"], code: "0");
            }
            holiday.Year = input.Year;
            holiday.Description = input.Description;
            holiday.Code = input.Year.ToString();
            holiday.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

            await _holidayRepository.UpdateAsync(holiday);

            return ObjectMapper.Map<Holiday, HolidayDto>(holiday);
        }
    }
}
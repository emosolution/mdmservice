using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Data;
using System.Linq;

namespace DMSpro.OMS.MdmService.HolidayDetails
{

    [Authorize(MdmServicePermissions.Holidays.Default)]
    public partial class HolidayDetailsAppService
    {
        public virtual async Task<HolidayDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<HolidayDetail, HolidayDetailDto>(await _holidayDetailRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.Holidays.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _holidayDetailRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Holidays.Create)]
        public virtual async Task<HolidayDetailDto> CreateAsync(HolidayDetailCreateDto input)
        {
            if (input.HolidayId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Holiday"]]);
            }
            if (input.StartDate == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Start date"]]);
            }
            if (input.EndDate == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["End date"]]);
            }
            Check.NotNull(input.HolidayId, nameof(input.HolidayId));
            Check.NotNull(input.StartDate, nameof(input.StartDate));
            Check.NotNull(input.EndDate, nameof(input.EndDate));
            Check.Length(input.Description, nameof(input.Description), HolidayDetailConsts.DescriptionMaxLength);
            CheckEffectiveDate(input.StartDate, input.EndDate);
            await CheckOverlappingDates(input.HolidayId, input.StartDate, input.EndDate);
            var holiday = await _holidayRepository.GetAsync(input.HolidayId);
            CheckDatesWithinHolidayYear(holiday.Year, input.StartDate, input.EndDate);

            var holidayDetail = new HolidayDetail(
                GuidGenerator.Create(),
             input.HolidayId, input.StartDate.Date, input.EndDate.Date, input.Description);
            await _holidayDetailRepository.InsertAsync(holidayDetail);
            return ObjectMapper.Map<HolidayDetail, HolidayDetailDto>(holidayDetail);
        }

        [Authorize(MdmServicePermissions.Holidays.Edit)]
        public virtual async Task<HolidayDetailDto> UpdateAsync(Guid id, HolidayDetailUpdateDto input)
        {
            if (input.StartDate == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Start date"]]);
            }
            if (input.EndDate == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["End date"]]);
            }

            Check.NotNull(input.StartDate, nameof(input.StartDate));
            Check.NotNull(input.EndDate, nameof(input.EndDate));
            Check.Length(input.Description, nameof(input.Description), HolidayDetailConsts.DescriptionMaxLength);
            CheckEffectiveDate(input.StartDate, input.EndDate);
            var holidayDetail = await _holidayDetailRepository.GetAsync(id);
            await CheckOverlappingDates(holidayDetail.HolidayId,
                input.StartDate, input.EndDate, id);
            var holiday = await _holidayRepository.GetAsync(holidayDetail.HolidayId);
            CheckDatesWithinHolidayYear(holiday.Year, input.StartDate, input.EndDate);

            holidayDetail.StartDate = input.StartDate.Date;
            holidayDetail.EndDate = input.EndDate.Date;
            holidayDetail.Description = input.Description;

            holidayDetail.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _holidayDetailRepository.UpdateAsync(holidayDetail);
            return ObjectMapper.Map<HolidayDetail, HolidayDetailDto>(holidayDetail);
        }

        private async Task CheckOverlappingDates(Guid holidayId,
            DateTime startDate, DateTime endDate, Guid? id = null)
        {
            var details = await _holidayDetailRepository.GetListAsync(
                x => x.HolidayId == holidayId &&
                x.Id != id);
            if (details.Any(
                x => (x.StartDate >= startDate && x.StartDate <= endDate) ||
                (x.EndDate >= startDate && x.EndDate <= endDate) ||
                (x.StartDate <= startDate && x.EndDate >= endDate)))
            {
                throw new UserFriendlyException(message: L["Error:HolidayDetailsAppService:550"], code: "0");
            }
        }

        private void CheckDatesWithinHolidayYear(int year, DateTime startDate, DateTime endDate)
        {
            var startOfYear = new DateTime(year, 1, 1);
            var endOfYear = new DateTime(year, 12, 31);
            if (startDate.Date < startOfYear || startDate > endOfYear ||
                endDate < startDate || endDate > endOfYear)
            {
                throw new UserFriendlyException(message: L["Error:HolidayDetailsAppService:551"], code: "0");
            }
        }
    }
}
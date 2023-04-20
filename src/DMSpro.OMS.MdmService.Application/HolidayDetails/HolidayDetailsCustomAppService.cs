using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Data;
using System.Runtime.CompilerServices;

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

            var holidayDetail = await _holidayDetailRepository.GetAsync(id);

            holidayDetail.StartDate = input.StartDate.Date;
            holidayDetail.EndDate = input.EndDate.Date;
            holidayDetail.Description = input.Description;

            holidayDetail.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _holidayDetailRepository.UpdateAsync(holidayDetail);
            return ObjectMapper.Map<HolidayDetail, HolidayDetailDto>(holidayDetail);
        }

        private void CheckInputDate(DateTime startDate, DateTime endDate)
        {
            if (endDate.Date < startDate.Date)
            {

            }
        }
    }
}
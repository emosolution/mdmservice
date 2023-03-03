using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class HolidayManager : DomainService
    {
        private readonly IHolidayRepository _holidayRepository;

        public HolidayManager(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }

        public async Task<Holiday> CreateAsync(
        int year, string description)
        {
            Check.Range(year, nameof(year), HolidayConsts.YearMinLength, HolidayConsts.YearMaxLength);
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.Length(description, nameof(description), HolidayConsts.DescriptionMaxLength, HolidayConsts.DescriptionMinLength);

            var holiday = new Holiday(
             GuidGenerator.Create(),
             year, description
             );

            return await _holidayRepository.InsertAsync(holiday);
        }

        public async Task<Holiday> UpdateAsync(
            Guid id,
            int year, string description, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.Range(year, nameof(year), HolidayConsts.YearMinLength, HolidayConsts.YearMaxLength);
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.Length(description, nameof(description), HolidayConsts.DescriptionMaxLength, HolidayConsts.DescriptionMinLength);

            var holiday = await _holidayRepository.GetAsync(id);

            holiday.Year = year;
            holiday.Description = description;

            holiday.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _holidayRepository.UpdateAsync(holiday);
        }

    }
}
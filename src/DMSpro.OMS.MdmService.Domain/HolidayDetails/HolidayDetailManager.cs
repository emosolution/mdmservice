using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailManager : DomainService
    {
        private readonly IHolidayDetailRepository _holidayDetailRepository;

        public HolidayDetailManager(IHolidayDetailRepository holidayDetailRepository)
        {
            _holidayDetailRepository = holidayDetailRepository;
        }

        public async Task<HolidayDetail> CreateAsync(
        Guid holidayId, DateTime startDate, DateTime endDate, string description)
        {
            Check.NotNull(holidayId, nameof(holidayId));
            Check.NotNull(startDate, nameof(startDate));
            Check.NotNull(endDate, nameof(endDate));

            var holidayDetail = new HolidayDetail(
             GuidGenerator.Create(),
             holidayId, startDate, endDate, description
             );

            return await _holidayDetailRepository.InsertAsync(holidayDetail);
        }

        public async Task<HolidayDetail> UpdateAsync(
            Guid id,
            Guid holidayId, DateTime startDate, DateTime endDate, string description, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(holidayId, nameof(holidayId));
            Check.NotNull(startDate, nameof(startDate));
            Check.NotNull(endDate, nameof(endDate));

            var queryable = await _holidayDetailRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var holidayDetail = await AsyncExecuter.FirstOrDefaultAsync(query);

            holidayDetail.HolidayId = holidayId;
            holidayDetail.StartDate = startDate;
            holidayDetail.EndDate = endDate;
            holidayDetail.Description = description;

            holidayDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _holidayDetailRepository.UpdateAsync(holidayDetail);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class EfCoreHolidayDetailRepository : EfCoreRepository<MdmServiceDbContext, HolidayDetail, Guid>, IHolidayDetailRepository
    {
        public EfCoreHolidayDetailRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<HolidayDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(holidayDetail => new HolidayDetailWithNavigationProperties
                {
                    HolidayDetail = holidayDetail,
                    Holiday = dbContext.Holidays.FirstOrDefault(c => c.Id == holidayDetail.HolidayId)
                }).FirstOrDefault();
        }

        public async Task<List<HolidayDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string description = null,
            Guid? holidayId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, startDateMin, startDateMax, endDateMin, endDateMax, description, holidayId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? HolidayDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<HolidayDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from holidayDetail in (await GetDbSetAsync())
                   join holiday in (await GetDbContextAsync()).Holidays on holidayDetail.HolidayId equals holiday.Id into holidays
                   from holiday in holidays.DefaultIfEmpty()

                   select new HolidayDetailWithNavigationProperties
                   {
                       HolidayDetail = holidayDetail,
                       Holiday = holiday
                   };
        }

        protected virtual IQueryable<HolidayDetailWithNavigationProperties> ApplyFilter(
            IQueryable<HolidayDetailWithNavigationProperties> query,
            string filterText,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string description = null,
            Guid? holidayId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.HolidayDetail.Description.Contains(filterText))
                    .WhereIf(startDateMin.HasValue, e => e.HolidayDetail.StartDate >= startDateMin.Value)
                    .WhereIf(startDateMax.HasValue, e => e.HolidayDetail.StartDate <= startDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.HolidayDetail.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.HolidayDetail.EndDate <= endDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.HolidayDetail.Description.Contains(description))
                    .WhereIf(holidayId != null && holidayId != Guid.Empty, e => e.Holiday != null && e.Holiday.Id == holidayId);
        }

        public async Task<List<HolidayDetail>> GetListAsync(
            string filterText = null,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string description = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, startDateMin, startDateMax, endDateMin, endDateMax, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? HolidayDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string description = null,
            Guid? holidayId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, startDateMin, startDateMax, endDateMin, endDateMax, description, holidayId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<HolidayDetail> ApplyFilter(
            IQueryable<HolidayDetail> query,
            string filterText,
            DateTime? startDateMin = null,
            DateTime? startDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string description = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText))
                    .WhereIf(startDateMin.HasValue, e => e.StartDate >= startDateMin.Value)
                    .WhereIf(startDateMax.HasValue, e => e.StartDate <= startDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}
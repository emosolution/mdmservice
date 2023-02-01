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

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public partial class EfCoreMCPDetailRepository : EfCoreRepository<MdmServiceDbContext, MCPDetail, Guid>, IMCPDetailRepository
    {
        public EfCoreMCPDetailRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<MCPDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(mcpDetail => new MCPDetailWithNavigationProperties
                {
                    MCPDetail = mcpDetail,
                    Customer = dbContext.Customers.FirstOrDefault(c => c.Id == mcpDetail.CustomerId),
                    MCPHeader = dbContext.MCPHeaders.FirstOrDefault(c => c.Id == mcpDetail.MCPHeaderId)
                }).FirstOrDefault();
        }

        public async Task<List<MCPDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            bool? monday = null,
            bool? tuesday = null,
            bool? wednesday = null,
            bool? thursday = null,
            bool? friday = null,
            bool? saturday = null,
            bool? sunday = null,
            bool? week1 = null,
            bool? week2 = null,
            bool? week3 = null,
            bool? week4 = null,
            Guid? customerId = null,
            Guid? mCPHeaderId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, distanceMin, distanceMax, visitOrderMin, visitOrderMax, monday, tuesday, wednesday, thursday, friday, saturday, sunday, week1, week2, week3, week4, customerId, mCPHeaderId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MCPDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<MCPDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from mcpDetail in (await GetDbSetAsync())
                   join customer in (await GetDbContextAsync()).Customers on mcpDetail.CustomerId equals customer.Id into customers
                   from customer in customers.DefaultIfEmpty()
                   join mCPHeader in (await GetDbContextAsync()).MCPHeaders on mcpDetail.MCPHeaderId equals mCPHeader.Id into mCPHeaders
                   from mCPHeader in mCPHeaders.DefaultIfEmpty()

                   select new MCPDetailWithNavigationProperties
                   {
                       MCPDetail = mcpDetail,
                       Customer = customer,
                       MCPHeader = mCPHeader
                   };
        }

        protected virtual IQueryable<MCPDetailWithNavigationProperties> ApplyFilter(
            IQueryable<MCPDetailWithNavigationProperties> query,
            string filterText,
            string code = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            bool? monday = null,
            bool? tuesday = null,
            bool? wednesday = null,
            bool? thursday = null,
            bool? friday = null,
            bool? saturday = null,
            bool? sunday = null,
            bool? week1 = null,
            bool? week2 = null,
            bool? week3 = null,
            bool? week4 = null,
            Guid? customerId = null,
            Guid? mCPHeaderId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.MCPDetail.Code.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.MCPDetail.Code.Contains(code))
                    .WhereIf(effectiveDateMin.HasValue, e => e.MCPDetail.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.MCPDetail.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.MCPDetail.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.MCPDetail.EndDate <= endDateMax.Value)
                    .WhereIf(distanceMin.HasValue, e => e.MCPDetail.Distance >= distanceMin.Value)
                    .WhereIf(distanceMax.HasValue, e => e.MCPDetail.Distance <= distanceMax.Value)
                    .WhereIf(visitOrderMin.HasValue, e => e.MCPDetail.VisitOrder >= visitOrderMin.Value)
                    .WhereIf(visitOrderMax.HasValue, e => e.MCPDetail.VisitOrder <= visitOrderMax.Value)
                    .WhereIf(monday.HasValue, e => e.MCPDetail.Monday == monday)
                    .WhereIf(tuesday.HasValue, e => e.MCPDetail.Tuesday == tuesday)
                    .WhereIf(wednesday.HasValue, e => e.MCPDetail.Wednesday == wednesday)
                    .WhereIf(thursday.HasValue, e => e.MCPDetail.Thursday == thursday)
                    .WhereIf(friday.HasValue, e => e.MCPDetail.Friday == friday)
                    .WhereIf(saturday.HasValue, e => e.MCPDetail.Saturday == saturday)
                    .WhereIf(sunday.HasValue, e => e.MCPDetail.Sunday == sunday)
                    .WhereIf(week1.HasValue, e => e.MCPDetail.Week1 == week1)
                    .WhereIf(week2.HasValue, e => e.MCPDetail.Week2 == week2)
                    .WhereIf(week3.HasValue, e => e.MCPDetail.Week3 == week3)
                    .WhereIf(week4.HasValue, e => e.MCPDetail.Week4 == week4)
                    .WhereIf(customerId != null && customerId != Guid.Empty, e => e.Customer != null && e.Customer.Id == customerId)
                    .WhereIf(mCPHeaderId != null && mCPHeaderId != Guid.Empty, e => e.MCPHeader != null && e.MCPHeader.Id == mCPHeaderId);
        }

        public async Task<List<MCPDetail>> GetListAsync(
            string filterText = null,
            string code = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            bool? monday = null,
            bool? tuesday = null,
            bool? wednesday = null,
            bool? thursday = null,
            bool? friday = null,
            bool? saturday = null,
            bool? sunday = null,
            bool? week1 = null,
            bool? week2 = null,
            bool? week3 = null,
            bool? week4 = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, distanceMin, distanceMax, visitOrderMin, visitOrderMax, monday, tuesday, wednesday, thursday, friday, saturday, sunday, week1, week2, week3, week4);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MCPDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            bool? monday = null,
            bool? tuesday = null,
            bool? wednesday = null,
            bool? thursday = null,
            bool? friday = null,
            bool? saturday = null,
            bool? sunday = null,
            bool? week1 = null,
            bool? week2 = null,
            bool? week3 = null,
            bool? week4 = null,
            Guid? customerId = null,
            Guid? mCPHeaderId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, distanceMin, distanceMax, visitOrderMin, visitOrderMax, monday, tuesday, wednesday, thursday, friday, saturday, sunday, week1, week2, week3, week4, customerId, mCPHeaderId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<MCPDetail> ApplyFilter(
            IQueryable<MCPDetail> query,
            string filterText,
            string code = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            bool? monday = null,
            bool? tuesday = null,
            bool? wednesday = null,
            bool? thursday = null,
            bool? friday = null,
            bool? saturday = null,
            bool? sunday = null,
            bool? week1 = null,
            bool? week2 = null,
            bool? week3 = null,
            bool? week4 = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value)
                    .WhereIf(distanceMin.HasValue, e => e.Distance >= distanceMin.Value)
                    .WhereIf(distanceMax.HasValue, e => e.Distance <= distanceMax.Value)
                    .WhereIf(visitOrderMin.HasValue, e => e.VisitOrder >= visitOrderMin.Value)
                    .WhereIf(visitOrderMax.HasValue, e => e.VisitOrder <= visitOrderMax.Value)
                    .WhereIf(monday.HasValue, e => e.Monday == monday)
                    .WhereIf(tuesday.HasValue, e => e.Tuesday == tuesday)
                    .WhereIf(wednesday.HasValue, e => e.Wednesday == wednesday)
                    .WhereIf(thursday.HasValue, e => e.Thursday == thursday)
                    .WhereIf(friday.HasValue, e => e.Friday == friday)
                    .WhereIf(saturday.HasValue, e => e.Saturday == saturday)
                    .WhereIf(sunday.HasValue, e => e.Sunday == sunday)
                    .WhereIf(week1.HasValue, e => e.Week1 == week1)
                    .WhereIf(week2.HasValue, e => e.Week2 == week2)
                    .WhereIf(week3.HasValue, e => e.Week3 == week3)
                    .WhereIf(week4.HasValue, e => e.Week4 == week4);
        }
    }
}
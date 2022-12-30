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

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class EfCoreVisitPlanRepository : EfCoreRepository<MdmServiceDbContext, VisitPlan, Guid>, IVisitPlanRepository
    {
        public EfCoreVisitPlanRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<VisitPlanWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(visitPlan => new VisitPlanWithNavigationProperties
                {
                    VisitPlan = visitPlan,
                    MCPDetail = dbContext.MCPDetails.FirstOrDefault(c => c.Id == visitPlan.MCPDetailId)
                }).FirstOrDefault();
        }

        public async Task<List<VisitPlanWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? dateVisitMin = null,
            DateTime? dateVisitMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            DayOfWeek? dayOfWeek = null,
            int? weekMin = null,
            int? weekMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            Guid? mCPDetailId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, dateVisitMin, dateVisitMax, distanceMin, distanceMax, visitOrderMin, visitOrderMax, dayOfWeek, weekMin, weekMax, monthMin, monthMax, yearMin, yearMax, mCPDetailId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VisitPlanConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VisitPlanWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from visitPlan in (await GetDbSetAsync())
                   join mCPDetail in (await GetDbContextAsync()).MCPDetails on visitPlan.MCPDetailId equals mCPDetail.Id into mCPDetails
                   from mCPDetail in mCPDetails.DefaultIfEmpty()

                   select new VisitPlanWithNavigationProperties
                   {
                       VisitPlan = visitPlan,
                       MCPDetail = mCPDetail
                   };
        }

        protected virtual IQueryable<VisitPlanWithNavigationProperties> ApplyFilter(
            IQueryable<VisitPlanWithNavigationProperties> query,
            string filterText,
            DateTime? dateVisitMin = null,
            DateTime? dateVisitMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            DayOfWeek? dayOfWeek = null,
            int? weekMin = null,
            int? weekMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            Guid? mCPDetailId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(dateVisitMin.HasValue, e => e.VisitPlan.DateVisit >= dateVisitMin.Value)
                    .WhereIf(dateVisitMax.HasValue, e => e.VisitPlan.DateVisit <= dateVisitMax.Value)
                    .WhereIf(distanceMin.HasValue, e => e.VisitPlan.Distance >= distanceMin.Value)
                    .WhereIf(distanceMax.HasValue, e => e.VisitPlan.Distance <= distanceMax.Value)
                    .WhereIf(visitOrderMin.HasValue, e => e.VisitPlan.VisitOrder >= visitOrderMin.Value)
                    .WhereIf(visitOrderMax.HasValue, e => e.VisitPlan.VisitOrder <= visitOrderMax.Value)
                    .WhereIf(dayOfWeek.HasValue, e => e.VisitPlan.DayOfWeek == dayOfWeek)
                    .WhereIf(weekMin.HasValue, e => e.VisitPlan.Week >= weekMin.Value)
                    .WhereIf(weekMax.HasValue, e => e.VisitPlan.Week <= weekMax.Value)
                    .WhereIf(monthMin.HasValue, e => e.VisitPlan.Month >= monthMin.Value)
                    .WhereIf(monthMax.HasValue, e => e.VisitPlan.Month <= monthMax.Value)
                    .WhereIf(yearMin.HasValue, e => e.VisitPlan.Year >= yearMin.Value)
                    .WhereIf(yearMax.HasValue, e => e.VisitPlan.Year <= yearMax.Value)
                    .WhereIf(mCPDetailId != null && mCPDetailId != Guid.Empty, e => e.MCPDetail != null && e.MCPDetail.Id == mCPDetailId);
        }

        public async Task<List<VisitPlan>> GetListAsync(
            string filterText = null,
            DateTime? dateVisitMin = null,
            DateTime? dateVisitMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            DayOfWeek? dayOfWeek = null,
            int? weekMin = null,
            int? weekMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, dateVisitMin, dateVisitMax, distanceMin, distanceMax, visitOrderMin, visitOrderMax, dayOfWeek, weekMin, weekMax, monthMin, monthMax, yearMin, yearMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VisitPlanConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? dateVisitMin = null,
            DateTime? dateVisitMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            DayOfWeek? dayOfWeek = null,
            int? weekMin = null,
            int? weekMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            Guid? mCPDetailId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, dateVisitMin, dateVisitMax, distanceMin, distanceMax, visitOrderMin, visitOrderMax, dayOfWeek, weekMin, weekMax, monthMin, monthMax, yearMin, yearMax, mCPDetailId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VisitPlan> ApplyFilter(
            IQueryable<VisitPlan> query,
            string filterText,
            DateTime? dateVisitMin = null,
            DateTime? dateVisitMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            DayOfWeek? dayOfWeek = null,
            int? weekMin = null,
            int? weekMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(dateVisitMin.HasValue, e => e.DateVisit >= dateVisitMin.Value)
                    .WhereIf(dateVisitMax.HasValue, e => e.DateVisit <= dateVisitMax.Value)
                    .WhereIf(distanceMin.HasValue, e => e.Distance >= distanceMin.Value)
                    .WhereIf(distanceMax.HasValue, e => e.Distance <= distanceMax.Value)
                    .WhereIf(visitOrderMin.HasValue, e => e.VisitOrder >= visitOrderMin.Value)
                    .WhereIf(visitOrderMax.HasValue, e => e.VisitOrder <= visitOrderMax.Value)
                    .WhereIf(dayOfWeek.HasValue, e => e.DayOfWeek == dayOfWeek)
                    .WhereIf(weekMin.HasValue, e => e.Week >= weekMin.Value)
                    .WhereIf(weekMax.HasValue, e => e.Week <= weekMax.Value)
                    .WhereIf(monthMin.HasValue, e => e.Month >= monthMin.Value)
                    .WhereIf(monthMax.HasValue, e => e.Month <= monthMax.Value)
                    .WhereIf(yearMin.HasValue, e => e.Year >= yearMin.Value)
                    .WhereIf(yearMax.HasValue, e => e.Year <= yearMax.Value);
        }
    }
}
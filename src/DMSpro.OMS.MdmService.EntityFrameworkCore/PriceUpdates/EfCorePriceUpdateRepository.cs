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

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public partial class EfCorePriceUpdateRepository : EfCoreRepository<MdmServiceDbContext, PriceUpdate, Guid>, IPriceUpdateRepository
    {
        public EfCorePriceUpdateRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<PriceUpdateWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(priceUpdate => new PriceUpdateWithNavigationProperties
                {
                    PriceUpdate = priceUpdate,
                    PriceList = dbContext.PriceLists.FirstOrDefault(c => c.Id == priceUpdate.PriceListId)
                }).FirstOrDefault();
        }

        public async Task<List<PriceUpdateWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string description = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            PriceUpdateStatus? status = null,
            DateTime? updateStatusDateMin = null,
            DateTime? updateStatusDateMax = null,
            Guid? priceListId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, description, effectiveDateMin, effectiveDateMax, status, updateStatusDateMin, updateStatusDateMax, priceListId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PriceUpdateConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<PriceUpdateWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from priceUpdate in (await GetDbSetAsync())
                   join priceList in (await GetDbContextAsync()).PriceLists on priceUpdate.PriceListId equals priceList.Id into priceLists
                   from priceList in priceLists.DefaultIfEmpty()

                   select new PriceUpdateWithNavigationProperties
                   {
                       PriceUpdate = priceUpdate,
                       PriceList = priceList
                   };
        }

        protected virtual IQueryable<PriceUpdateWithNavigationProperties> ApplyFilter(
            IQueryable<PriceUpdateWithNavigationProperties> query,
            string filterText,
            string code = null,
            string description = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            PriceUpdateStatus? status = null,
            DateTime? updateStatusDateMin = null,
            DateTime? updateStatusDateMax = null,
            Guid? priceListId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PriceUpdate.Code.Contains(filterText) || e.PriceUpdate.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.PriceUpdate.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.PriceUpdate.Description.Contains(description))
                    .WhereIf(effectiveDateMin.HasValue, e => e.PriceUpdate.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.PriceUpdate.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(status.HasValue, e => e.PriceUpdate.Status == status)
                    .WhereIf(updateStatusDateMin.HasValue, e => e.PriceUpdate.UpdateStatusDate >= updateStatusDateMin.Value)
                    .WhereIf(updateStatusDateMax.HasValue, e => e.PriceUpdate.UpdateStatusDate <= updateStatusDateMax.Value)
                    .WhereIf(priceListId != null && priceListId != Guid.Empty, e => e.PriceList != null && e.PriceList.Id == priceListId);
        }

        public async Task<List<PriceUpdate>> GetListAsync(
            string filterText = null,
            string code = null,
            string description = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            PriceUpdateStatus? status = null,
            DateTime? updateStatusDateMin = null,
            DateTime? updateStatusDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, description, effectiveDateMin, effectiveDateMax, status, updateStatusDateMin, updateStatusDateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PriceUpdateConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string description = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            PriceUpdateStatus? status = null,
            DateTime? updateStatusDateMin = null,
            DateTime? updateStatusDateMax = null,
            Guid? priceListId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, description, effectiveDateMin, effectiveDateMax, status, updateStatusDateMin, updateStatusDateMax, priceListId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<PriceUpdate> ApplyFilter(
            IQueryable<PriceUpdate> query,
            string filterText,
            string code = null,
            string description = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            PriceUpdateStatus? status = null,
            DateTime? updateStatusDateMin = null,
            DateTime? updateStatusDateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(status.HasValue, e => e.Status == status)
                    .WhereIf(updateStatusDateMin.HasValue, e => e.UpdateStatusDate >= updateStatusDateMin.Value)
                    .WhereIf(updateStatusDateMax.HasValue, e => e.UpdateStatusDate <= updateStatusDateMax.Value);
        }
    }
}
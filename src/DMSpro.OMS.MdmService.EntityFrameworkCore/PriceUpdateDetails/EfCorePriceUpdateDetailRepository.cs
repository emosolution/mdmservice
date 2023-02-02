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

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public partial class EfCorePriceUpdateDetailRepository : EfCoreRepository<MdmServiceDbContext, PriceUpdateDetail, Guid>, IPriceUpdateDetailRepository
    {
        public EfCorePriceUpdateDetailRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<PriceUpdateDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(priceUpdateDetail => new PriceUpdateDetailWithNavigationProperties
                {
                    PriceUpdateDetail = priceUpdateDetail,
                    PriceUpdate = dbContext.PriceUpdates.FirstOrDefault(c => c.Id == priceUpdateDetail.PriceUpdateId),
                    PriceListDetail = dbContext.PriceListDetails.FirstOrDefault(c => c.Id == priceUpdateDetail.PriceListDetailId)
                }).FirstOrDefault();
        }

        public async Task<List<PriceUpdateDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? priceBeforeUpdateMin = null,
            int? priceBeforeUpdateMax = null,
            int? newPriceMin = null,
            int? newPriceMax = null,
            DateTime? updatedDateMin = null,
            DateTime? updatedDateMax = null,
            Guid? priceUpdateId = null,
            Guid? priceListDetailId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, priceBeforeUpdateMin, priceBeforeUpdateMax, newPriceMin, newPriceMax, updatedDateMin, updatedDateMax, priceUpdateId, priceListDetailId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PriceUpdateDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<PriceUpdateDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from priceUpdateDetail in (await GetDbSetAsync())
                   join priceUpdate in (await GetDbContextAsync()).PriceUpdates on priceUpdateDetail.PriceUpdateId equals priceUpdate.Id into priceUpdates
                   from priceUpdate in priceUpdates.DefaultIfEmpty()
                   join priceListDetail in (await GetDbContextAsync()).PriceListDetails on priceUpdateDetail.PriceListDetailId equals priceListDetail.Id into priceListDetails
                   from priceListDetail in priceListDetails.DefaultIfEmpty()

                   select new PriceUpdateDetailWithNavigationProperties
                   {
                       PriceUpdateDetail = priceUpdateDetail,
                       PriceUpdate = priceUpdate,
                       PriceListDetail = priceListDetail
                   };
        }

        protected virtual IQueryable<PriceUpdateDetailWithNavigationProperties> ApplyFilter(
            IQueryable<PriceUpdateDetailWithNavigationProperties> query,
            string filterText,
            int? priceBeforeUpdateMin = null,
            int? priceBeforeUpdateMax = null,
            int? newPriceMin = null,
            int? newPriceMax = null,
            DateTime? updatedDateMin = null,
            DateTime? updatedDateMax = null,
            Guid? priceUpdateId = null,
            Guid? priceListDetailId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(priceBeforeUpdateMin.HasValue, e => e.PriceUpdateDetail.PriceBeforeUpdate >= priceBeforeUpdateMin.Value)
                    .WhereIf(priceBeforeUpdateMax.HasValue, e => e.PriceUpdateDetail.PriceBeforeUpdate <= priceBeforeUpdateMax.Value)
                    .WhereIf(newPriceMin.HasValue, e => e.PriceUpdateDetail.NewPrice >= newPriceMin.Value)
                    .WhereIf(newPriceMax.HasValue, e => e.PriceUpdateDetail.NewPrice <= newPriceMax.Value)
                    .WhereIf(updatedDateMin.HasValue, e => e.PriceUpdateDetail.UpdatedDate >= updatedDateMin.Value)
                    .WhereIf(updatedDateMax.HasValue, e => e.PriceUpdateDetail.UpdatedDate <= updatedDateMax.Value)
                    .WhereIf(priceUpdateId != null && priceUpdateId != Guid.Empty, e => e.PriceUpdate != null && e.PriceUpdate.Id == priceUpdateId)
                    .WhereIf(priceListDetailId != null && priceListDetailId != Guid.Empty, e => e.PriceListDetail != null && e.PriceListDetail.Id == priceListDetailId);
        }

        public async Task<List<PriceUpdateDetail>> GetListAsync(
            string filterText = null,
            int? priceBeforeUpdateMin = null,
            int? priceBeforeUpdateMax = null,
            int? newPriceMin = null,
            int? newPriceMax = null,
            DateTime? updatedDateMin = null,
            DateTime? updatedDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, priceBeforeUpdateMin, priceBeforeUpdateMax, newPriceMin, newPriceMax, updatedDateMin, updatedDateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PriceUpdateDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? priceBeforeUpdateMin = null,
            int? priceBeforeUpdateMax = null,
            int? newPriceMin = null,
            int? newPriceMax = null,
            DateTime? updatedDateMin = null,
            DateTime? updatedDateMax = null,
            Guid? priceUpdateId = null,
            Guid? priceListDetailId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, priceBeforeUpdateMin, priceBeforeUpdateMax, newPriceMin, newPriceMax, updatedDateMin, updatedDateMax, priceUpdateId, priceListDetailId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<PriceUpdateDetail> ApplyFilter(
            IQueryable<PriceUpdateDetail> query,
            string filterText,
            int? priceBeforeUpdateMin = null,
            int? priceBeforeUpdateMax = null,
            int? newPriceMin = null,
            int? newPriceMax = null,
            DateTime? updatedDateMin = null,
            DateTime? updatedDateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(priceBeforeUpdateMin.HasValue, e => e.PriceBeforeUpdate >= priceBeforeUpdateMin.Value)
                    .WhereIf(priceBeforeUpdateMax.HasValue, e => e.PriceBeforeUpdate <= priceBeforeUpdateMax.Value)
                    .WhereIf(newPriceMin.HasValue, e => e.NewPrice >= newPriceMin.Value)
                    .WhereIf(newPriceMax.HasValue, e => e.NewPrice <= newPriceMax.Value)
                    .WhereIf(updatedDateMin.HasValue, e => e.UpdatedDate >= updatedDateMin.Value)
                    .WhereIf(updatedDateMax.HasValue, e => e.UpdatedDate <= updatedDateMax.Value);
        }
    }
}
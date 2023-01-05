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

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class EfCorePriceListDetailRepository : EfCoreRepository<MdmServiceDbContext, PriceListDetail, Guid>, IPriceListDetailRepository
    {
        public EfCorePriceListDetailRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<PriceListDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(priceListDetail => new PriceListDetailWithNavigationProperties
                {
                    PriceListDetail = priceListDetail,
                    PriceList = dbContext.PriceLists.FirstOrDefault(c => c.Id == priceListDetail.PriceListId),
                    UOM = dbContext.UOMs.FirstOrDefault(c => c.Id == priceListDetail.UOMId),
                    Item = dbContext.Items.FirstOrDefault(c => c.Id == priceListDetail.ItemId)
                }).FirstOrDefault();
        }

        public async Task<List<PriceListDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? priceMin = null,
            int? priceMax = null,
            int? basedOnPriceMin = null,
            int? basedOnPriceMax = null,
            string description = null,
            Guid? priceListId = null,
            Guid? uOMId = null,
            Guid? itemId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, priceMin, priceMax, basedOnPriceMin, basedOnPriceMax, description, priceListId, uOMId, itemId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PriceListDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<PriceListDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from priceListDetail in (await GetDbSetAsync())
                   join priceList in (await GetDbContextAsync()).PriceLists on priceListDetail.PriceListId equals priceList.Id into priceLists
                   from priceList in priceLists.DefaultIfEmpty()
                   join uOM in (await GetDbContextAsync()).UOMs on priceListDetail.UOMId equals uOM.Id into uOMs
                   from uOM in uOMs.DefaultIfEmpty()
                   join item in (await GetDbContextAsync()).Items on priceListDetail.ItemId equals item.Id into items
                   from item in items.DefaultIfEmpty()

                   select new PriceListDetailWithNavigationProperties
                   {
                       PriceListDetail = priceListDetail,
                       PriceList = priceList,
                       UOM = uOM,
                       Item = item
                   };
        }

        protected virtual IQueryable<PriceListDetailWithNavigationProperties> ApplyFilter(
            IQueryable<PriceListDetailWithNavigationProperties> query,
            string filterText,
            int? priceMin = null,
            int? priceMax = null,
            int? basedOnPriceMin = null,
            int? basedOnPriceMax = null,
            string description = null,
            Guid? priceListId = null,
            Guid? uOMId = null,
            Guid? itemId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PriceListDetail.Description.Contains(filterText))
                    .WhereIf(priceMin.HasValue, e => e.PriceListDetail.Price >= priceMin.Value)
                    .WhereIf(priceMax.HasValue, e => e.PriceListDetail.Price <= priceMax.Value)
                    .WhereIf(basedOnPriceMin.HasValue, e => e.PriceListDetail.BasedOnPrice >= basedOnPriceMin.Value)
                    .WhereIf(basedOnPriceMax.HasValue, e => e.PriceListDetail.BasedOnPrice <= basedOnPriceMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.PriceListDetail.Description.Contains(description))
                    .WhereIf(priceListId != null && priceListId != Guid.Empty, e => e.PriceList != null && e.PriceList.Id == priceListId)
                    .WhereIf(uOMId != null && uOMId != Guid.Empty, e => e.UOM != null && e.UOM.Id == uOMId)
                    .WhereIf(itemId != null && itemId != Guid.Empty, e => e.Item != null && e.Item.Id == itemId);
        }

        public async Task<List<PriceListDetail>> GetListAsync(
            string filterText = null,
            int? priceMin = null,
            int? priceMax = null,
            int? basedOnPriceMin = null,
            int? basedOnPriceMax = null,
            string description = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, priceMin, priceMax, basedOnPriceMin, basedOnPriceMax, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PriceListDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? priceMin = null,
            int? priceMax = null,
            int? basedOnPriceMin = null,
            int? basedOnPriceMax = null,
            string description = null,
            Guid? priceListId = null,
            Guid? uOMId = null,
            Guid? itemId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, priceMin, priceMax, basedOnPriceMin, basedOnPriceMax, description, priceListId, uOMId, itemId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<PriceListDetail> ApplyFilter(
            IQueryable<PriceListDetail> query,
            string filterText,
            int? priceMin = null,
            int? priceMax = null,
            int? basedOnPriceMin = null,
            int? basedOnPriceMax = null,
            string description = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText))
                    .WhereIf(priceMin.HasValue, e => e.Price >= priceMin.Value)
                    .WhereIf(priceMax.HasValue, e => e.Price <= priceMax.Value)
                    .WhereIf(basedOnPriceMin.HasValue, e => e.BasedOnPrice >= basedOnPriceMin.Value)
                    .WhereIf(basedOnPriceMax.HasValue, e => e.BasedOnPrice <= basedOnPriceMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}
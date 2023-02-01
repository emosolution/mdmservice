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

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public partial class EfCoreItemGroupListRepository : EfCoreRepository<MdmServiceDbContext, ItemGroupList, Guid>, IItemGroupListRepository
    {
        public EfCoreItemGroupListRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ItemGroupListWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(itemGroupList => new ItemGroupListWithNavigationProperties
                {
                    ItemGroupList = itemGroupList,
                    ItemGroup = dbContext.ItemGroups.FirstOrDefault(c => c.Id == itemGroupList.ItemGroupId),
                    Item = dbContext.Items.FirstOrDefault(c => c.Id == itemGroupList.ItemId),
                    UOM = dbContext.UOMs.FirstOrDefault(c => c.Id == itemGroupList.UomId)
                }).FirstOrDefault();
        }

        public async Task<List<ItemGroupListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? rateMin = null,
            int? rateMax = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            Guid? itemGroupId = null,
            Guid? itemId = null,
            Guid? uomId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, rateMin, rateMax, priceMin, priceMax, itemGroupId, itemId, uomId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupListConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ItemGroupListWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from itemGroupList in (await GetDbSetAsync())
                   join itemGroup in (await GetDbContextAsync()).ItemGroups on itemGroupList.ItemGroupId equals itemGroup.Id into itemGroups
                   from itemGroup in itemGroups.DefaultIfEmpty()
                   join item in (await GetDbContextAsync()).Items on itemGroupList.ItemId equals item.Id into items
                   from item in items.DefaultIfEmpty()
                   join uOM in (await GetDbContextAsync()).UOMs on itemGroupList.UomId equals uOM.Id into uOMs
                   from uOM in uOMs.DefaultIfEmpty()

                   select new ItemGroupListWithNavigationProperties
                   {
                       ItemGroupList = itemGroupList,
                       ItemGroup = itemGroup,
                       Item = item,
                       UOM = uOM
                   };
        }

        protected virtual IQueryable<ItemGroupListWithNavigationProperties> ApplyFilter(
            IQueryable<ItemGroupListWithNavigationProperties> query,
            string filterText,
            int? rateMin = null,
            int? rateMax = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            Guid? itemGroupId = null,
            Guid? itemId = null,
            Guid? uomId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(rateMin.HasValue, e => e.ItemGroupList.Rate >= rateMin.Value)
                    .WhereIf(rateMax.HasValue, e => e.ItemGroupList.Rate <= rateMax.Value)
                    .WhereIf(priceMin.HasValue, e => e.ItemGroupList.Price >= priceMin.Value)
                    .WhereIf(priceMax.HasValue, e => e.ItemGroupList.Price <= priceMax.Value)
                    .WhereIf(itemGroupId != null && itemGroupId != Guid.Empty, e => e.ItemGroup != null && e.ItemGroup.Id == itemGroupId)
                    .WhereIf(itemId != null && itemId != Guid.Empty, e => e.Item != null && e.Item.Id == itemId)
                    .WhereIf(uomId != null && uomId != Guid.Empty, e => e.UOM != null && e.UOM.Id == uomId);
        }

        public async Task<List<ItemGroupList>> GetListAsync(
            string filterText = null,
            int? rateMin = null,
            int? rateMax = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, rateMin, rateMax, priceMin, priceMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupListConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? rateMin = null,
            int? rateMax = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            Guid? itemGroupId = null,
            Guid? itemId = null,
            Guid? uomId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, rateMin, rateMax, priceMin, priceMax, itemGroupId, itemId, uomId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemGroupList> ApplyFilter(
            IQueryable<ItemGroupList> query,
            string filterText,
            int? rateMin = null,
            int? rateMax = null,
            decimal? priceMin = null,
            decimal? priceMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(rateMin.HasValue, e => e.Rate >= rateMin.Value)
                    .WhereIf(rateMax.HasValue, e => e.Rate <= rateMax.Value)
                    .WhereIf(priceMin.HasValue, e => e.Price >= priceMin.Value)
                    .WhereIf(priceMax.HasValue, e => e.Price <= priceMax.Value);
        }
    }
}
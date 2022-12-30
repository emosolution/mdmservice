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
    public class EfCoreItemGroupListRepository : EfCoreRepository<MdmServiceDbContext, ItemGroupList, Guid>, IItemGroupListRepository
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
                    ItemMaster = dbContext.ItemMasters.FirstOrDefault(c => c.Id == itemGroupList.ItemId),
                    UOM = dbContext.UOMs.FirstOrDefault(c => c.Id == itemGroupList.UOMId)
                }).FirstOrDefault();
        }

        public async Task<List<ItemGroupListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? rateMin = null,
            int? rateMax = null,
            Guid? itemGroupId = null,
            Guid? itemId = null,
            Guid? uOMId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, rateMin, rateMax, itemGroupId, itemId, uOMId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupListConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ItemGroupListWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from itemGroupList in (await GetDbSetAsync())
                   join itemGroup in (await GetDbContextAsync()).ItemGroups on itemGroupList.ItemGroupId equals itemGroup.Id into itemGroups
                   from itemGroup in itemGroups.DefaultIfEmpty()
                   join itemMaster in (await GetDbContextAsync()).ItemMasters on itemGroupList.ItemId equals itemMaster.Id into itemMasters
                   from itemMaster in itemMasters.DefaultIfEmpty()
                   join uOM in (await GetDbContextAsync()).UOMs on itemGroupList.UOMId equals uOM.Id into uOMs
                   from uOM in uOMs.DefaultIfEmpty()

                   select new ItemGroupListWithNavigationProperties
                   {
                       ItemGroupList = itemGroupList,
                       ItemGroup = itemGroup,
                       ItemMaster = itemMaster,
                       UOM = uOM
                   };
        }

        protected virtual IQueryable<ItemGroupListWithNavigationProperties> ApplyFilter(
            IQueryable<ItemGroupListWithNavigationProperties> query,
            string filterText,
            int? rateMin = null,
            int? rateMax = null,
            Guid? itemGroupId = null,
            Guid? itemId = null,
            Guid? uOMId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(rateMin.HasValue, e => e.ItemGroupList.Rate >= rateMin.Value)
                    .WhereIf(rateMax.HasValue, e => e.ItemGroupList.Rate <= rateMax.Value)
                    .WhereIf(itemGroupId != null && itemGroupId != Guid.Empty, e => e.ItemGroup != null && e.ItemGroup.Id == itemGroupId)
                    .WhereIf(itemId != null && itemId != Guid.Empty, e => e.ItemMaster != null && e.ItemMaster.Id == itemId)
                    .WhereIf(uOMId != null && uOMId != Guid.Empty, e => e.UOM != null && e.UOM.Id == uOMId);
        }

        public async Task<List<ItemGroupList>> GetListAsync(
            string filterText = null,
            int? rateMin = null,
            int? rateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, rateMin, rateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupListConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? rateMin = null,
            int? rateMax = null,
            Guid? itemGroupId = null,
            Guid? itemId = null,
            Guid? uOMId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, rateMin, rateMax, itemGroupId, itemId, uOMId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemGroupList> ApplyFilter(
            IQueryable<ItemGroupList> query,
            string filterText,
            int? rateMin = null,
            int? rateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(rateMin.HasValue, e => e.Rate >= rateMin.Value)
                    .WhereIf(rateMax.HasValue, e => e.Rate <= rateMax.Value);
        }
    }
}
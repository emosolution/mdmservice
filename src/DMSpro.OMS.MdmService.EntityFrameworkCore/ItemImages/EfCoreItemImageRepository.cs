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

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class EfCoreItemImageRepository : EfCoreRepository<MdmServiceDbContext, ItemImage, Guid>, IItemImageRepository
    {
        public EfCoreItemImageRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ItemImageWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(itemImage => new ItemImageWithNavigationProperties
                {
                    ItemImage = itemImage,
                    ItemMaster = dbContext.ItemMasters.FirstOrDefault(c => c.Id == itemImage.ItemId)
                }).FirstOrDefault();
        }

        public async Task<List<ItemImageWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            string uRL = null,
            int? displayOrderMin = null,
            int? displayOrderMax = null,
            Guid? itemId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, active, uRL, displayOrderMin, displayOrderMax, itemId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemImageConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ItemImageWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from itemImage in (await GetDbSetAsync())
                   join itemMaster in (await GetDbContextAsync()).ItemMasters on itemImage.ItemId equals itemMaster.Id into itemMasters
                   from itemMaster in itemMasters.DefaultIfEmpty()

                   select new ItemImageWithNavigationProperties
                   {
                       ItemImage = itemImage,
                       ItemMaster = itemMaster
                   };
        }

        protected virtual IQueryable<ItemImageWithNavigationProperties> ApplyFilter(
            IQueryable<ItemImageWithNavigationProperties> query,
            string filterText,
            string description = null,
            bool? active = null,
            string uRL = null,
            int? displayOrderMin = null,
            int? displayOrderMax = null,
            Guid? itemId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ItemImage.Description.Contains(filterText) || e.ItemImage.URL.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.ItemImage.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.ItemImage.Active == active)
                    .WhereIf(!string.IsNullOrWhiteSpace(uRL), e => e.ItemImage.URL.Contains(uRL))
                    .WhereIf(displayOrderMin.HasValue, e => e.ItemImage.DisplayOrder >= displayOrderMin.Value)
                    .WhereIf(displayOrderMax.HasValue, e => e.ItemImage.DisplayOrder <= displayOrderMax.Value)
                    .WhereIf(itemId != null && itemId != Guid.Empty, e => e.ItemMaster != null && e.ItemMaster.Id == itemId);
        }

        public async Task<List<ItemImage>> GetListAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            string uRL = null,
            int? displayOrderMin = null,
            int? displayOrderMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, description, active, uRL, displayOrderMin, displayOrderMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemImageConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            string uRL = null,
            int? displayOrderMin = null,
            int? displayOrderMax = null,
            Guid? itemId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, active, uRL, displayOrderMin, displayOrderMax, itemId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemImage> ApplyFilter(
            IQueryable<ItemImage> query,
            string filterText,
            string description = null,
            bool? active = null,
            string uRL = null,
            int? displayOrderMin = null,
            int? displayOrderMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText) || e.URL.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(!string.IsNullOrWhiteSpace(uRL), e => e.URL.Contains(uRL))
                    .WhereIf(displayOrderMin.HasValue, e => e.DisplayOrder >= displayOrderMin.Value)
                    .WhereIf(displayOrderMax.HasValue, e => e.DisplayOrder <= displayOrderMax.Value);
        }
    }
}
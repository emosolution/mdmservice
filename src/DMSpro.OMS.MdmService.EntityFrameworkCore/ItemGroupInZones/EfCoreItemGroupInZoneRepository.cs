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

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class EfCoreItemGroupInZoneRepository : EfCoreRepository<MdmServiceDbContext, ItemGroupInZone, Guid>, IItemGroupInZoneRepository
    {
        public EfCoreItemGroupInZoneRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ItemGroupInZoneWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(itemGroupInZone => new ItemGroupInZoneWithNavigationProperties
                {
                    ItemGroupInZone = itemGroupInZone,
                    SalesOrgHierarchy = dbContext.SalesOrgHierarchies.FirstOrDefault(c => c.Id == itemGroupInZone.SellingZoneId),
                    ItemGroup = dbContext.ItemGroups.FirstOrDefault(c => c.Id == itemGroupInZone.ItemGroupId)
                }).FirstOrDefault();
        }

        public async Task<List<ItemGroupInZoneWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? active = null,
            string description = null,
            Guid? sellingZoneId = null,
            Guid? itemGroupId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, active, description, sellingZoneId, itemGroupId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupInZoneConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ItemGroupInZoneWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from itemGroupInZone in (await GetDbSetAsync())
                   join salesOrgHierarchy in (await GetDbContextAsync()).SalesOrgHierarchies on itemGroupInZone.SellingZoneId equals salesOrgHierarchy.Id into salesOrgHierarchies
                   from salesOrgHierarchy in salesOrgHierarchies.DefaultIfEmpty()
                   join itemGroup in (await GetDbContextAsync()).ItemGroups on itemGroupInZone.ItemGroupId equals itemGroup.Id into itemGroups
                   from itemGroup in itemGroups.DefaultIfEmpty()

                   select new ItemGroupInZoneWithNavigationProperties
                   {
                       ItemGroupInZone = itemGroupInZone,
                       SalesOrgHierarchy = salesOrgHierarchy,
                       ItemGroup = itemGroup
                   };
        }

        protected virtual IQueryable<ItemGroupInZoneWithNavigationProperties> ApplyFilter(
            IQueryable<ItemGroupInZoneWithNavigationProperties> query,
            string filterText,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? active = null,
            string description = null,
            Guid? sellingZoneId = null,
            Guid? itemGroupId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ItemGroupInZone.Description.Contains(filterText))
                    .WhereIf(effectiveDateMin.HasValue, e => e.ItemGroupInZone.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.ItemGroupInZone.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.ItemGroupInZone.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.ItemGroupInZone.EndDate <= endDateMax.Value)
                    .WhereIf(active.HasValue, e => e.ItemGroupInZone.Active == active)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.ItemGroupInZone.Description.Contains(description))
                    .WhereIf(sellingZoneId != null && sellingZoneId != Guid.Empty, e => e.SalesOrgHierarchy != null && e.SalesOrgHierarchy.Id == sellingZoneId)
                    .WhereIf(itemGroupId != null && itemGroupId != Guid.Empty, e => e.ItemGroup != null && e.ItemGroup.Id == itemGroupId);
        }

        public async Task<List<ItemGroupInZone>> GetListAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? active = null,
            string description = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, active, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupInZoneConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? active = null,
            string description = null,
            Guid? sellingZoneId = null,
            Guid? itemGroupId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, active, description, sellingZoneId, itemGroupId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemGroupInZone> ApplyFilter(
            IQueryable<ItemGroupInZone> query,
            string filterText,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? active = null,
            string description = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText))
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value)
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}
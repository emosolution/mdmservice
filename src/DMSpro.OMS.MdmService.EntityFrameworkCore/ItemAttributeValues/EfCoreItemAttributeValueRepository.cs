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

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class EfCoreItemAttributeValueRepository : EfCoreRepository<MdmServiceDbContext, ItemAttributeValue, Guid>, IItemAttributeValueRepository
    {
        public EfCoreItemAttributeValueRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ItemAttributeValueWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(itemAttributeValue => new ItemAttributeValueWithNavigationProperties
                {
                    ItemAttributeValue = itemAttributeValue,
                    ItemAttribute = dbContext.ItemAttributes.FirstOrDefault(c => c.Id == itemAttributeValue.ItemAttributeId),
                    ItemAttributeValue1 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemAttributeValue.ParentId)
                }).FirstOrDefault();
        }

        public async Task<List<ItemAttributeValueWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string attrValName = null,
            Guid? itemAttributeId = null,
            Guid? parentId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, attrValName, itemAttributeId, parentId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemAttributeValueConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ItemAttributeValueWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from itemAttributeValue in (await GetDbSetAsync())
                   join itemAttribute in (await GetDbContextAsync()).ItemAttributes on itemAttributeValue.ItemAttributeId equals itemAttribute.Id into itemAttributes
                   from itemAttribute in itemAttributes.DefaultIfEmpty()
                   join itemAttributeValue1 in (await GetDbContextAsync()).ItemAttributeValues on itemAttributeValue.ParentId equals itemAttributeValue1.Id into itemAttributeValues1
                   from itemAttributeValue1 in itemAttributeValues1.DefaultIfEmpty()

                   select new ItemAttributeValueWithNavigationProperties
                   {
                       ItemAttributeValue = itemAttributeValue,
                       ItemAttribute = itemAttribute,
                       ItemAttributeValue1 = itemAttributeValue1
                   };
        }

        protected virtual IQueryable<ItemAttributeValueWithNavigationProperties> ApplyFilter(
            IQueryable<ItemAttributeValueWithNavigationProperties> query,
            string filterText,
            string attrValName = null,
            Guid? itemAttributeId = null,
            Guid? parentId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ItemAttributeValue.AttrValName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(attrValName), e => e.ItemAttributeValue.AttrValName.Contains(attrValName))
                    .WhereIf(itemAttributeId != null && itemAttributeId != Guid.Empty, e => e.ItemAttribute != null && e.ItemAttribute.Id == itemAttributeId)
                    .WhereIf(parentId != null && parentId != Guid.Empty, e => e.ItemAttributeValue1 != null && e.ItemAttributeValue1.Id == parentId);
        }

        public async Task<List<ItemAttributeValue>> GetListAsync(
            string filterText = null,
            string attrValName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, attrValName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemAttributeValueConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string attrValName = null,
            Guid? itemAttributeId = null,
            Guid? parentId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, attrValName, itemAttributeId, parentId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemAttributeValue> ApplyFilter(
            IQueryable<ItemAttributeValue> query,
            string filterText,
            string attrValName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AttrValName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(attrValName), e => e.AttrValName.Contains(attrValName));
        }
    }
}
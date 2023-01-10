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

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public class EfCoreItemGroupAttributeRepository : EfCoreRepository<MdmServiceDbContext, ItemGroupAttribute, Guid>, IItemGroupAttributeRepository
    {
        public EfCoreItemGroupAttributeRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ItemGroupAttributeWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(itemGroupAttribute => new ItemGroupAttributeWithNavigationProperties
                {
                    ItemGroupAttribute = itemGroupAttribute,
                    ItemGroup = dbContext.ItemGroups.FirstOrDefault(c => c.Id == itemGroupAttribute.ItemGroupId),
                    ItemAttributeValue = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr0Id),
                    ItemAttributeValue1 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr1Id),
                    ItemAttributeValue2 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr2Id),
                    ItemAttributeValue3 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr3Id),
                    ItemAttributeValue4 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr4Id),
                    ItemAttributeValue5 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr6Id),
                    ItemAttributeValue6 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr7Id),
                    ItemAttributeValue7 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr8Id),
                    ItemAttributeValue8 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr9Id),
                    ItemAttributeValue9 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr10Id),
                    ItemAttributeValue10 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr11Id),
                    ItemAttributeValue11 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr12Id),
                    ItemAttributeValue12 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr13Id),
                    ItemAttributeValue13 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr14Id),
                    ItemAttributeValue14 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr15Id),
                    ItemAttributeValue15 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr16Id),
                    ItemAttributeValue16 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr17Id),
                    ItemAttributeValue17 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr18Id),
                    ItemAttributeValue18 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr19Id),
                    ItemAttributeValue19 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttribute.Attr5Id)
                }).FirstOrDefault();
        }

        public async Task<List<ItemGroupAttributeWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string dummy = null,
            Guid? itemGroupId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr6Id = null,
            Guid? attr7Id = null,
            Guid? attr8Id = null,
            Guid? attr9Id = null,
            Guid? attr10Id = null,
            Guid? attr11Id = null,
            Guid? attr12Id = null,
            Guid? attr13Id = null,
            Guid? attr14Id = null,
            Guid? attr15Id = null,
            Guid? attr16Id = null,
            Guid? attr17Id = null,
            Guid? attr18Id = null,
            Guid? attr19Id = null,
            Guid? attr5Id = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, dummy, itemGroupId, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id, attr5Id);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupAttributeConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ItemGroupAttributeWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from itemGroupAttribute in (await GetDbSetAsync())
                   join itemGroup in (await GetDbContextAsync()).ItemGroups on itemGroupAttribute.ItemGroupId equals itemGroup.Id into itemGroups
                   from itemGroup in itemGroups.DefaultIfEmpty()
                   join itemAttributeValue in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr0Id equals itemAttributeValue.Id into itemAttributeValues
                   from itemAttributeValue in itemAttributeValues.DefaultIfEmpty()
                   join itemAttributeValue1 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr1Id equals itemAttributeValue1.Id into itemAttributeValues1
                   from itemAttributeValue1 in itemAttributeValues1.DefaultIfEmpty()
                   join itemAttributeValue2 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr2Id equals itemAttributeValue2.Id into itemAttributeValues2
                   from itemAttributeValue2 in itemAttributeValues2.DefaultIfEmpty()
                   join itemAttributeValue3 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr3Id equals itemAttributeValue3.Id into itemAttributeValues3
                   from itemAttributeValue3 in itemAttributeValues3.DefaultIfEmpty()
                   join itemAttributeValue4 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr4Id equals itemAttributeValue4.Id into itemAttributeValues4
                   from itemAttributeValue4 in itemAttributeValues4.DefaultIfEmpty()
                   join itemAttributeValue5 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr6Id equals itemAttributeValue5.Id into itemAttributeValues5
                   from itemAttributeValue5 in itemAttributeValues5.DefaultIfEmpty()
                   join itemAttributeValue6 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr7Id equals itemAttributeValue6.Id into itemAttributeValues6
                   from itemAttributeValue6 in itemAttributeValues6.DefaultIfEmpty()
                   join itemAttributeValue7 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr8Id equals itemAttributeValue7.Id into itemAttributeValues7
                   from itemAttributeValue7 in itemAttributeValues7.DefaultIfEmpty()
                   join itemAttributeValue8 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr9Id equals itemAttributeValue8.Id into itemAttributeValues8
                   from itemAttributeValue8 in itemAttributeValues8.DefaultIfEmpty()
                   join itemAttributeValue9 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr10Id equals itemAttributeValue9.Id into itemAttributeValues9
                   from itemAttributeValue9 in itemAttributeValues9.DefaultIfEmpty()
                   join itemAttributeValue10 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr11Id equals itemAttributeValue10.Id into itemAttributeValues10
                   from itemAttributeValue10 in itemAttributeValues10.DefaultIfEmpty()
                   join itemAttributeValue11 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr12Id equals itemAttributeValue11.Id into itemAttributeValues11
                   from itemAttributeValue11 in itemAttributeValues11.DefaultIfEmpty()
                   join itemAttributeValue12 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr13Id equals itemAttributeValue12.Id into itemAttributeValues12
                   from itemAttributeValue12 in itemAttributeValues12.DefaultIfEmpty()
                   join itemAttributeValue13 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr14Id equals itemAttributeValue13.Id into itemAttributeValues13
                   from itemAttributeValue13 in itemAttributeValues13.DefaultIfEmpty()
                   join itemAttributeValue14 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr15Id equals itemAttributeValue14.Id into itemAttributeValues14
                   from itemAttributeValue14 in itemAttributeValues14.DefaultIfEmpty()
                   join itemAttributeValue15 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr16Id equals itemAttributeValue15.Id into itemAttributeValues15
                   from itemAttributeValue15 in itemAttributeValues15.DefaultIfEmpty()
                   join itemAttributeValue16 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr17Id equals itemAttributeValue16.Id into itemAttributeValues16
                   from itemAttributeValue16 in itemAttributeValues16.DefaultIfEmpty()
                   join itemAttributeValue17 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr18Id equals itemAttributeValue17.Id into itemAttributeValues17
                   from itemAttributeValue17 in itemAttributeValues17.DefaultIfEmpty()
                   join itemAttributeValue18 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr19Id equals itemAttributeValue18.Id into itemAttributeValues18
                   from itemAttributeValue18 in itemAttributeValues18.DefaultIfEmpty()
                   join itemAttributeValue19 in (await GetDbContextAsync()).ItemAttributeValues on itemGroupAttribute.Attr5Id equals itemAttributeValue19.Id into itemAttributeValues19
                   from itemAttributeValue19 in itemAttributeValues19.DefaultIfEmpty()

                   select new ItemGroupAttributeWithNavigationProperties
                   {
                       ItemGroupAttribute = itemGroupAttribute,
                       ItemGroup = itemGroup,
                       ItemAttributeValue = itemAttributeValue,
                       ItemAttributeValue1 = itemAttributeValue1,
                       ItemAttributeValue2 = itemAttributeValue2,
                       ItemAttributeValue3 = itemAttributeValue3,
                       ItemAttributeValue4 = itemAttributeValue4,
                       ItemAttributeValue5 = itemAttributeValue5,
                       ItemAttributeValue6 = itemAttributeValue6,
                       ItemAttributeValue7 = itemAttributeValue7,
                       ItemAttributeValue8 = itemAttributeValue8,
                       ItemAttributeValue9 = itemAttributeValue9,
                       ItemAttributeValue10 = itemAttributeValue10,
                       ItemAttributeValue11 = itemAttributeValue11,
                       ItemAttributeValue12 = itemAttributeValue12,
                       ItemAttributeValue13 = itemAttributeValue13,
                       ItemAttributeValue14 = itemAttributeValue14,
                       ItemAttributeValue15 = itemAttributeValue15,
                       ItemAttributeValue16 = itemAttributeValue16,
                       ItemAttributeValue17 = itemAttributeValue17,
                       ItemAttributeValue18 = itemAttributeValue18,
                       ItemAttributeValue19 = itemAttributeValue19
                   };
        }

        protected virtual IQueryable<ItemGroupAttributeWithNavigationProperties> ApplyFilter(
            IQueryable<ItemGroupAttributeWithNavigationProperties> query,
            string filterText,
            string dummy = null,
            Guid? itemGroupId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr6Id = null,
            Guid? attr7Id = null,
            Guid? attr8Id = null,
            Guid? attr9Id = null,
            Guid? attr10Id = null,
            Guid? attr11Id = null,
            Guid? attr12Id = null,
            Guid? attr13Id = null,
            Guid? attr14Id = null,
            Guid? attr15Id = null,
            Guid? attr16Id = null,
            Guid? attr17Id = null,
            Guid? attr18Id = null,
            Guid? attr19Id = null,
            Guid? attr5Id = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ItemGroupAttribute.dummy.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(dummy), e => e.ItemGroupAttribute.dummy.Contains(dummy))
                    .WhereIf(itemGroupId != null && itemGroupId != Guid.Empty, e => e.ItemGroup != null && e.ItemGroup.Id == itemGroupId)
                    .WhereIf(attr0Id != null && attr0Id != Guid.Empty, e => e.ItemAttributeValue != null && e.ItemAttributeValue.Id == attr0Id)
                    .WhereIf(attr1Id != null && attr1Id != Guid.Empty, e => e.ItemAttributeValue1 != null && e.ItemAttributeValue1.Id == attr1Id)
                    .WhereIf(attr2Id != null && attr2Id != Guid.Empty, e => e.ItemAttributeValue2 != null && e.ItemAttributeValue2.Id == attr2Id)
                    .WhereIf(attr3Id != null && attr3Id != Guid.Empty, e => e.ItemAttributeValue3 != null && e.ItemAttributeValue3.Id == attr3Id)
                    .WhereIf(attr4Id != null && attr4Id != Guid.Empty, e => e.ItemAttributeValue4 != null && e.ItemAttributeValue4.Id == attr4Id)
                    .WhereIf(attr6Id != null && attr6Id != Guid.Empty, e => e.ItemAttributeValue5 != null && e.ItemAttributeValue5.Id == attr6Id)
                    .WhereIf(attr7Id != null && attr7Id != Guid.Empty, e => e.ItemAttributeValue6 != null && e.ItemAttributeValue6.Id == attr7Id)
                    .WhereIf(attr8Id != null && attr8Id != Guid.Empty, e => e.ItemAttributeValue7 != null && e.ItemAttributeValue7.Id == attr8Id)
                    .WhereIf(attr9Id != null && attr9Id != Guid.Empty, e => e.ItemAttributeValue8 != null && e.ItemAttributeValue8.Id == attr9Id)
                    .WhereIf(attr10Id != null && attr10Id != Guid.Empty, e => e.ItemAttributeValue9 != null && e.ItemAttributeValue9.Id == attr10Id)
                    .WhereIf(attr11Id != null && attr11Id != Guid.Empty, e => e.ItemAttributeValue10 != null && e.ItemAttributeValue10.Id == attr11Id)
                    .WhereIf(attr12Id != null && attr12Id != Guid.Empty, e => e.ItemAttributeValue11 != null && e.ItemAttributeValue11.Id == attr12Id)
                    .WhereIf(attr13Id != null && attr13Id != Guid.Empty, e => e.ItemAttributeValue12 != null && e.ItemAttributeValue12.Id == attr13Id)
                    .WhereIf(attr14Id != null && attr14Id != Guid.Empty, e => e.ItemAttributeValue13 != null && e.ItemAttributeValue13.Id == attr14Id)
                    .WhereIf(attr15Id != null && attr15Id != Guid.Empty, e => e.ItemAttributeValue14 != null && e.ItemAttributeValue14.Id == attr15Id)
                    .WhereIf(attr16Id != null && attr16Id != Guid.Empty, e => e.ItemAttributeValue15 != null && e.ItemAttributeValue15.Id == attr16Id)
                    .WhereIf(attr17Id != null && attr17Id != Guid.Empty, e => e.ItemAttributeValue16 != null && e.ItemAttributeValue16.Id == attr17Id)
                    .WhereIf(attr18Id != null && attr18Id != Guid.Empty, e => e.ItemAttributeValue17 != null && e.ItemAttributeValue17.Id == attr18Id)
                    .WhereIf(attr19Id != null && attr19Id != Guid.Empty, e => e.ItemAttributeValue18 != null && e.ItemAttributeValue18.Id == attr19Id)
                    .WhereIf(attr5Id != null && attr5Id != Guid.Empty, e => e.ItemAttributeValue19 != null && e.ItemAttributeValue19.Id == attr5Id);
        }

        public async Task<List<ItemGroupAttribute>> GetListAsync(
            string filterText = null,
            string dummy = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, dummy);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupAttributeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string dummy = null,
            Guid? itemGroupId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr6Id = null,
            Guid? attr7Id = null,
            Guid? attr8Id = null,
            Guid? attr9Id = null,
            Guid? attr10Id = null,
            Guid? attr11Id = null,
            Guid? attr12Id = null,
            Guid? attr13Id = null,
            Guid? attr14Id = null,
            Guid? attr15Id = null,
            Guid? attr16Id = null,
            Guid? attr17Id = null,
            Guid? attr18Id = null,
            Guid? attr19Id = null,
            Guid? attr5Id = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, dummy, itemGroupId, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id, attr5Id);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemGroupAttribute> ApplyFilter(
            IQueryable<ItemGroupAttribute> query,
            string filterText,
            string dummy = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.dummy.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(dummy), e => e.dummy.Contains(dummy));
        }
    }
}
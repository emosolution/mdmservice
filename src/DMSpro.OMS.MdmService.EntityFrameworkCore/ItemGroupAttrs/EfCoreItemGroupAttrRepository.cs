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

namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    public class EfCoreItemGroupAttrRepository : EfCoreRepository<MdmServiceDbContext, ItemGroupAttr, Guid>, IItemGroupAttrRepository
    {
        public EfCoreItemGroupAttrRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ItemGroupAttrWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(itemGroupAttr => new ItemGroupAttrWithNavigationProperties
                {
                    ItemGroupAttr = itemGroupAttr,
                    ItemGroup = dbContext.ItemGroups.FirstOrDefault(c => c.Id == itemGroupAttr.ItemGroupId),
                    ProdAttributeValue = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr0),
                    ProdAttributeValue1 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr1),
                    ProdAttributeValue2 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr2),
                    ProdAttributeValue3 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr3),
                    ProdAttributeValue4 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr4),
                    ProdAttributeValue5 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr5),
                    ProdAttributeValue6 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr6),
                    ProdAttributeValue7 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr7),
                    ProdAttributeValue8 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr8),
                    ProdAttributeValue9 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr9),
                    ProdAttributeValue10 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr10),
                    ProdAttributeValue11 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr11),
                    ProdAttributeValue12 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr12),
                    ProdAttributeValue13 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr13),
                    ProdAttributeValue14 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr14),
                    ProdAttributeValue15 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr15),
                    ProdAttributeValue16 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr16),
                    ProdAttributeValue17 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr17),
                    ProdAttributeValue18 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr18),
                    ProdAttributeValue19 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemGroupAttr.Attr19)
                }).FirstOrDefault();
        }

        public async Task<List<ItemGroupAttrWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? dummy = null,
            Guid? itemGroupId = null,
            Guid? attr0 = null,
            Guid? attr1 = null,
            Guid? attr2 = null,
            Guid? attr3 = null,
            Guid? attr4 = null,
            Guid? attr5 = null,
            Guid? attr6 = null,
            Guid? attr7 = null,
            Guid? attr8 = null,
            Guid? attr9 = null,
            Guid? attr10 = null,
            Guid? attr11 = null,
            Guid? attr12 = null,
            Guid? attr13 = null,
            Guid? attr14 = null,
            Guid? attr15 = null,
            Guid? attr16 = null,
            Guid? attr17 = null,
            Guid? attr18 = null,
            Guid? attr19 = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, dummy, itemGroupId, attr0, attr1, attr2, attr3, attr4, attr5, attr6, attr7, attr8, attr9, attr10, attr11, attr12, attr13, attr14, attr15, attr16, attr17, attr18, attr19);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupAttrConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ItemGroupAttrWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from itemGroupAttr in (await GetDbSetAsync())
                   join itemGroup in (await GetDbContextAsync()).ItemGroups on itemGroupAttr.ItemGroupId equals itemGroup.Id into itemGroups
                   from itemGroup in itemGroups.DefaultIfEmpty()
                   join prodAttributeValue in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr0 equals prodAttributeValue.Id into prodAttributeValues
                   from prodAttributeValue in prodAttributeValues.DefaultIfEmpty()
                   join prodAttributeValue1 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr1 equals prodAttributeValue1.Id into prodAttributeValues1
                   from prodAttributeValue1 in prodAttributeValues1.DefaultIfEmpty()
                   join prodAttributeValue2 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr2 equals prodAttributeValue2.Id into prodAttributeValues2
                   from prodAttributeValue2 in prodAttributeValues2.DefaultIfEmpty()
                   join prodAttributeValue3 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr3 equals prodAttributeValue3.Id into prodAttributeValues3
                   from prodAttributeValue3 in prodAttributeValues3.DefaultIfEmpty()
                   join prodAttributeValue4 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr4 equals prodAttributeValue4.Id into prodAttributeValues4
                   from prodAttributeValue4 in prodAttributeValues4.DefaultIfEmpty()
                   join prodAttributeValue5 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr5 equals prodAttributeValue5.Id into prodAttributeValues5
                   from prodAttributeValue5 in prodAttributeValues5.DefaultIfEmpty()
                   join prodAttributeValue6 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr6 equals prodAttributeValue6.Id into prodAttributeValues6
                   from prodAttributeValue6 in prodAttributeValues6.DefaultIfEmpty()
                   join prodAttributeValue7 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr7 equals prodAttributeValue7.Id into prodAttributeValues7
                   from prodAttributeValue7 in prodAttributeValues7.DefaultIfEmpty()
                   join prodAttributeValue8 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr8 equals prodAttributeValue8.Id into prodAttributeValues8
                   from prodAttributeValue8 in prodAttributeValues8.DefaultIfEmpty()
                   join prodAttributeValue9 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr9 equals prodAttributeValue9.Id into prodAttributeValues9
                   from prodAttributeValue9 in prodAttributeValues9.DefaultIfEmpty()
                   join prodAttributeValue10 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr10 equals prodAttributeValue10.Id into prodAttributeValues10
                   from prodAttributeValue10 in prodAttributeValues10.DefaultIfEmpty()
                   join prodAttributeValue11 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr11 equals prodAttributeValue11.Id into prodAttributeValues11
                   from prodAttributeValue11 in prodAttributeValues11.DefaultIfEmpty()
                   join prodAttributeValue12 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr12 equals prodAttributeValue12.Id into prodAttributeValues12
                   from prodAttributeValue12 in prodAttributeValues12.DefaultIfEmpty()
                   join prodAttributeValue13 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr13 equals prodAttributeValue13.Id into prodAttributeValues13
                   from prodAttributeValue13 in prodAttributeValues13.DefaultIfEmpty()
                   join prodAttributeValue14 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr14 equals prodAttributeValue14.Id into prodAttributeValues14
                   from prodAttributeValue14 in prodAttributeValues14.DefaultIfEmpty()
                   join prodAttributeValue15 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr15 equals prodAttributeValue15.Id into prodAttributeValues15
                   from prodAttributeValue15 in prodAttributeValues15.DefaultIfEmpty()
                   join prodAttributeValue16 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr16 equals prodAttributeValue16.Id into prodAttributeValues16
                   from prodAttributeValue16 in prodAttributeValues16.DefaultIfEmpty()
                   join prodAttributeValue17 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr17 equals prodAttributeValue17.Id into prodAttributeValues17
                   from prodAttributeValue17 in prodAttributeValues17.DefaultIfEmpty()
                   join prodAttributeValue18 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr18 equals prodAttributeValue18.Id into prodAttributeValues18
                   from prodAttributeValue18 in prodAttributeValues18.DefaultIfEmpty()
                   join prodAttributeValue19 in (await GetDbContextAsync()).ProdAttributeValues on itemGroupAttr.Attr19 equals prodAttributeValue19.Id into prodAttributeValues19
                   from prodAttributeValue19 in prodAttributeValues19.DefaultIfEmpty()

                   select new ItemGroupAttrWithNavigationProperties
                   {
                       ItemGroupAttr = itemGroupAttr,
                       ItemGroup = itemGroup,
                       ProdAttributeValue = prodAttributeValue,
                       ProdAttributeValue1 = prodAttributeValue1,
                       ProdAttributeValue2 = prodAttributeValue2,
                       ProdAttributeValue3 = prodAttributeValue3,
                       ProdAttributeValue4 = prodAttributeValue4,
                       ProdAttributeValue5 = prodAttributeValue5,
                       ProdAttributeValue6 = prodAttributeValue6,
                       ProdAttributeValue7 = prodAttributeValue7,
                       ProdAttributeValue8 = prodAttributeValue8,
                       ProdAttributeValue9 = prodAttributeValue9,
                       ProdAttributeValue10 = prodAttributeValue10,
                       ProdAttributeValue11 = prodAttributeValue11,
                       ProdAttributeValue12 = prodAttributeValue12,
                       ProdAttributeValue13 = prodAttributeValue13,
                       ProdAttributeValue14 = prodAttributeValue14,
                       ProdAttributeValue15 = prodAttributeValue15,
                       ProdAttributeValue16 = prodAttributeValue16,
                       ProdAttributeValue17 = prodAttributeValue17,
                       ProdAttributeValue18 = prodAttributeValue18,
                       ProdAttributeValue19 = prodAttributeValue19
                   };
        }

        protected virtual IQueryable<ItemGroupAttrWithNavigationProperties> ApplyFilter(
            IQueryable<ItemGroupAttrWithNavigationProperties> query,
            string filterText,
            bool? dummy = null,
            Guid? itemGroupId = null,
            Guid? attr0 = null,
            Guid? attr1 = null,
            Guid? attr2 = null,
            Guid? attr3 = null,
            Guid? attr4 = null,
            Guid? attr5 = null,
            Guid? attr6 = null,
            Guid? attr7 = null,
            Guid? attr8 = null,
            Guid? attr9 = null,
            Guid? attr10 = null,
            Guid? attr11 = null,
            Guid? attr12 = null,
            Guid? attr13 = null,
            Guid? attr14 = null,
            Guid? attr15 = null,
            Guid? attr16 = null,
            Guid? attr17 = null,
            Guid? attr18 = null,
            Guid? attr19 = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(dummy.HasValue, e => e.ItemGroupAttr.Dummy == dummy)
                    .WhereIf(itemGroupId != null && itemGroupId != Guid.Empty, e => e.ItemGroup != null && e.ItemGroup.Id == itemGroupId)
                    .WhereIf(attr0 != null && attr0 != Guid.Empty, e => e.ProdAttributeValue != null && e.ProdAttributeValue.Id == attr0)
                    .WhereIf(attr1 != null && attr1 != Guid.Empty, e => e.ProdAttributeValue1 != null && e.ProdAttributeValue1.Id == attr1)
                    .WhereIf(attr2 != null && attr2 != Guid.Empty, e => e.ProdAttributeValue2 != null && e.ProdAttributeValue2.Id == attr2)
                    .WhereIf(attr3 != null && attr3 != Guid.Empty, e => e.ProdAttributeValue3 != null && e.ProdAttributeValue3.Id == attr3)
                    .WhereIf(attr4 != null && attr4 != Guid.Empty, e => e.ProdAttributeValue4 != null && e.ProdAttributeValue4.Id == attr4)
                    .WhereIf(attr5 != null && attr5 != Guid.Empty, e => e.ProdAttributeValue5 != null && e.ProdAttributeValue5.Id == attr5)
                    .WhereIf(attr6 != null && attr6 != Guid.Empty, e => e.ProdAttributeValue6 != null && e.ProdAttributeValue6.Id == attr6)
                    .WhereIf(attr7 != null && attr7 != Guid.Empty, e => e.ProdAttributeValue7 != null && e.ProdAttributeValue7.Id == attr7)
                    .WhereIf(attr8 != null && attr8 != Guid.Empty, e => e.ProdAttributeValue8 != null && e.ProdAttributeValue8.Id == attr8)
                    .WhereIf(attr9 != null && attr9 != Guid.Empty, e => e.ProdAttributeValue9 != null && e.ProdAttributeValue9.Id == attr9)
                    .WhereIf(attr10 != null && attr10 != Guid.Empty, e => e.ProdAttributeValue10 != null && e.ProdAttributeValue10.Id == attr10)
                    .WhereIf(attr11 != null && attr11 != Guid.Empty, e => e.ProdAttributeValue11 != null && e.ProdAttributeValue11.Id == attr11)
                    .WhereIf(attr12 != null && attr12 != Guid.Empty, e => e.ProdAttributeValue12 != null && e.ProdAttributeValue12.Id == attr12)
                    .WhereIf(attr13 != null && attr13 != Guid.Empty, e => e.ProdAttributeValue13 != null && e.ProdAttributeValue13.Id == attr13)
                    .WhereIf(attr14 != null && attr14 != Guid.Empty, e => e.ProdAttributeValue14 != null && e.ProdAttributeValue14.Id == attr14)
                    .WhereIf(attr15 != null && attr15 != Guid.Empty, e => e.ProdAttributeValue15 != null && e.ProdAttributeValue15.Id == attr15)
                    .WhereIf(attr16 != null && attr16 != Guid.Empty, e => e.ProdAttributeValue16 != null && e.ProdAttributeValue16.Id == attr16)
                    .WhereIf(attr17 != null && attr17 != Guid.Empty, e => e.ProdAttributeValue17 != null && e.ProdAttributeValue17.Id == attr17)
                    .WhereIf(attr18 != null && attr18 != Guid.Empty, e => e.ProdAttributeValue18 != null && e.ProdAttributeValue18.Id == attr18)
                    .WhereIf(attr19 != null && attr19 != Guid.Empty, e => e.ProdAttributeValue19 != null && e.ProdAttributeValue19.Id == attr19);
        }

        public async Task<List<ItemGroupAttr>> GetListAsync(
            string filterText = null,
            bool? dummy = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, dummy);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupAttrConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            bool? dummy = null,
            Guid? itemGroupId = null,
            Guid? attr0 = null,
            Guid? attr1 = null,
            Guid? attr2 = null,
            Guid? attr3 = null,
            Guid? attr4 = null,
            Guid? attr5 = null,
            Guid? attr6 = null,
            Guid? attr7 = null,
            Guid? attr8 = null,
            Guid? attr9 = null,
            Guid? attr10 = null,
            Guid? attr11 = null,
            Guid? attr12 = null,
            Guid? attr13 = null,
            Guid? attr14 = null,
            Guid? attr15 = null,
            Guid? attr16 = null,
            Guid? attr17 = null,
            Guid? attr18 = null,
            Guid? attr19 = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, dummy, itemGroupId, attr0, attr1, attr2, attr3, attr4, attr5, attr6, attr7, attr8, attr9, attr10, attr11, attr12, attr13, attr14, attr15, attr16, attr17, attr18, attr19);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemGroupAttr> ApplyFilter(
            IQueryable<ItemGroupAttr> query,
            string filterText,
            bool? dummy = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(dummy.HasValue, e => e.Dummy == dummy);
        }
    }
}
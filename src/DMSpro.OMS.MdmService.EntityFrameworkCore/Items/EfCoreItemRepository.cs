using DMSpro.OMS.MdmService.Items;
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

namespace DMSpro.OMS.MdmService.Items
{
    public partial class EfCoreItemRepository : EfCoreRepository<MdmServiceDbContext, Item, Guid>, IItemRepository
    {
        public EfCoreItemRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ItemWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(item => new ItemWithNavigationProperties
                {
                    Item = item,
                    SystemData = dbContext.SystemDatas.FirstOrDefault(c => c.Id == item.ItemTypeId),
                    VAT = dbContext.VATs.FirstOrDefault(c => c.Id == item.VatId),
                    UOMGroup = dbContext.UOMGroups.FirstOrDefault(c => c.Id == item.UomGroupId),
                    InventoryUnit = dbContext.UOMs.FirstOrDefault(c => c.Id == item.InventoryUOMId),
                    PurUnit = dbContext.UOMs.FirstOrDefault(c => c.Id == item.PurUOMId),
                    SalesUnit = dbContext.UOMs.FirstOrDefault(c => c.Id == item.SalesUOMId),
                    Attr0 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr0Id),
                    Attr1 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr1Id),
                    Attr2 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr2Id),
                    Attr3 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr3Id),
                    Attr4 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr4Id),
                    Attr5 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr5Id),
                    Attr6 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr6Id),
                    Attr7 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr7Id),
                    Attr8 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr8Id),
                    Attr9 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr9Id),
                    Attr10 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr10Id),
                    Attr11 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr11Id),
                    Attr12 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr12Id),
                    Attr13 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr13Id),
                    Attr14 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr14Id),
                    Attr15 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr15Id),
                    Attr16 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr16Id),
                    Attr17 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr17Id),
                    Attr18 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr18Id),
                    Attr19 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr19Id)
                }).FirstOrDefault();
        }

        public async Task<List<ItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string erpCode = null,
            string barcode = null,
            bool? isPurchasable = null,
            bool? isSaleable = null,
            bool? isInventoriable = null,
            decimal? basePriceMin = null,
            decimal? basePriceMax = null,
            bool? active = null,
            ManageBy? manageItemBy = null,
            ExpiredType? expiredType = null,
            int? expiredValueMin = null,
            int? expiredValueMax = null,
            IssueMethod? issueMethod = null,
            bool? canUpdate = null,
            decimal? purUnitRateMin = null,
            decimal? purUnitRateMax = null,
            decimal? salesUnitRateMin = null,
            decimal? salesUnitRateMax = null,
            Guid? itemTypeId = null,
            Guid? vatId = null,
            Guid? uomGroupId = null,
            Guid? inventoryUOMId = null,
            Guid? purUOMId = null,
            Guid? salesUOMId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr5Id = null,
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
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, shortName, erpCode, barcode, isPurchasable, isSaleable, isInventoriable, basePriceMin, basePriceMax, active, manageItemBy, expiredType, expiredValueMin, expiredValueMax, issueMethod, canUpdate, purUnitRateMin, purUnitRateMax, salesUnitRateMin, salesUnitRateMax, itemTypeId, vatId, uomGroupId, inventoryUOMId, purUOMId, salesUOMId, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ItemWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from item in (await GetDbSetAsync())
                   join systemData in (await GetDbContextAsync()).SystemDatas on item.ItemTypeId equals systemData.Id into systemDatas
                   from systemData in systemDatas.DefaultIfEmpty()
                   join vAT in (await GetDbContextAsync()).VATs on item.VatId equals vAT.Id into vATs
                   from vAT in vATs.DefaultIfEmpty()
                   join uOMGroup in (await GetDbContextAsync()).UOMGroups on item.UomGroupId equals uOMGroup.Id into uOMGroups
                   from uOMGroup in uOMGroups.DefaultIfEmpty()
                   join uOM in (await GetDbContextAsync()).UOMs on item.InventoryUOMId equals uOM.Id into uOMs
                   from uOM in uOMs.DefaultIfEmpty()
                   join uOM1 in (await GetDbContextAsync()).UOMs on item.PurUOMId equals uOM1.Id into uOMs1
                   from uOM1 in uOMs1.DefaultIfEmpty()
                   join uOM2 in (await GetDbContextAsync()).UOMs on item.SalesUOMId equals uOM2.Id into uOMs2
                   from uOM2 in uOMs2.DefaultIfEmpty()
                   join itemAttributeValue in (await GetDbContextAsync()).ItemAttributeValues on item.Attr0Id equals itemAttributeValue.Id into itemAttributeValues
                   from itemAttributeValue in itemAttributeValues.DefaultIfEmpty()
                   join itemAttributeValue1 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr1Id equals itemAttributeValue1.Id into itemAttributeValues1
                   from itemAttributeValue1 in itemAttributeValues1.DefaultIfEmpty()
                   join itemAttributeValue2 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr2Id equals itemAttributeValue2.Id into itemAttributeValues2
                   from itemAttributeValue2 in itemAttributeValues2.DefaultIfEmpty()
                   join itemAttributeValue3 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr3Id equals itemAttributeValue3.Id into itemAttributeValues3
                   from itemAttributeValue3 in itemAttributeValues3.DefaultIfEmpty()
                   join itemAttributeValue4 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr4Id equals itemAttributeValue4.Id into itemAttributeValues4
                   from itemAttributeValue4 in itemAttributeValues4.DefaultIfEmpty()
                   join itemAttributeValue5 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr5Id equals itemAttributeValue5.Id into itemAttributeValues5
                   from itemAttributeValue5 in itemAttributeValues5.DefaultIfEmpty()
                   join itemAttributeValue6 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr6Id equals itemAttributeValue6.Id into itemAttributeValues6
                   from itemAttributeValue6 in itemAttributeValues6.DefaultIfEmpty()
                   join itemAttributeValue7 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr7Id equals itemAttributeValue7.Id into itemAttributeValues7
                   from itemAttributeValue7 in itemAttributeValues7.DefaultIfEmpty()
                   join itemAttributeValue8 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr8Id equals itemAttributeValue8.Id into itemAttributeValues8
                   from itemAttributeValue8 in itemAttributeValues8.DefaultIfEmpty()
                   join itemAttributeValue9 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr9Id equals itemAttributeValue9.Id into itemAttributeValues9
                   from itemAttributeValue9 in itemAttributeValues9.DefaultIfEmpty()
                   join itemAttributeValue10 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr10Id equals itemAttributeValue10.Id into itemAttributeValues10
                   from itemAttributeValue10 in itemAttributeValues10.DefaultIfEmpty()
                   join itemAttributeValue11 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr11Id equals itemAttributeValue11.Id into itemAttributeValues11
                   from itemAttributeValue11 in itemAttributeValues11.DefaultIfEmpty()
                   join itemAttributeValue12 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr12Id equals itemAttributeValue12.Id into itemAttributeValues12
                   from itemAttributeValue12 in itemAttributeValues12.DefaultIfEmpty()
                   join itemAttributeValue13 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr13Id equals itemAttributeValue13.Id into itemAttributeValues13
                   from itemAttributeValue13 in itemAttributeValues13.DefaultIfEmpty()
                   join itemAttributeValue14 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr14Id equals itemAttributeValue14.Id into itemAttributeValues14
                   from itemAttributeValue14 in itemAttributeValues14.DefaultIfEmpty()
                   join itemAttributeValue15 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr15Id equals itemAttributeValue15.Id into itemAttributeValues15
                   from itemAttributeValue15 in itemAttributeValues15.DefaultIfEmpty()
                   join itemAttributeValue16 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr16Id equals itemAttributeValue16.Id into itemAttributeValues16
                   from itemAttributeValue16 in itemAttributeValues16.DefaultIfEmpty()
                   join itemAttributeValue17 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr17Id equals itemAttributeValue17.Id into itemAttributeValues17
                   from itemAttributeValue17 in itemAttributeValues17.DefaultIfEmpty()
                   join itemAttributeValue18 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr18Id equals itemAttributeValue18.Id into itemAttributeValues18
                   from itemAttributeValue18 in itemAttributeValues18.DefaultIfEmpty()
                   join itemAttributeValue19 in (await GetDbContextAsync()).ItemAttributeValues on item.Attr19Id equals itemAttributeValue19.Id into itemAttributeValues19
                   from itemAttributeValue19 in itemAttributeValues19.DefaultIfEmpty()

                   select new ItemWithNavigationProperties
                   {
                       Item = item,
                       SystemData = systemData,
                       VAT = vAT,
                       UOMGroup = uOMGroup,
                       InventoryUnit = uOM,
                       PurUnit = uOM1,
                       SalesUnit = uOM2,
                       Attr0 = itemAttributeValue,
                       Attr1 = itemAttributeValue1,
                       Attr2 = itemAttributeValue2,
                       Attr3 = itemAttributeValue3,
                       Attr4 = itemAttributeValue4,
                       Attr5 = itemAttributeValue5,
                       Attr6 = itemAttributeValue6,
                       Attr7 = itemAttributeValue7,
                       Attr8 = itemAttributeValue8,
                       Attr9 = itemAttributeValue9,
                       Attr10 = itemAttributeValue10,
                       Attr11 = itemAttributeValue11,
                       Attr12 = itemAttributeValue12,
                       Attr13 = itemAttributeValue13,
                       Attr14 = itemAttributeValue14,
                       Attr15 = itemAttributeValue15,
                       Attr16 = itemAttributeValue16,
                       Attr17 = itemAttributeValue17,
                       Attr18 = itemAttributeValue18,
                       Attr19 = itemAttributeValue19
                   };
        }

        protected virtual IQueryable<ItemWithNavigationProperties> ApplyFilter(
            IQueryable<ItemWithNavigationProperties> query,
            string filterText,
            string code = null,
            string name = null,
            string shortName = null,
            string erpCode = null,
            string barcode = null,
            bool? isPurchasable = null,
            bool? isSaleable = null,
            bool? isInventoriable = null,
            decimal? basePriceMin = null,
            decimal? basePriceMax = null,
            bool? active = null,
            ManageBy? manageItemBy = null,
            ExpiredType? expiredType = null,
            int? expiredValueMin = null,
            int? expiredValueMax = null,
            IssueMethod? issueMethod = null,
            bool? canUpdate = null,
            decimal? purUnitRateMin = null,
            decimal? purUnitRateMax = null,
            decimal? salesUnitRateMin = null,
            decimal? salesUnitRateMax = null,
            Guid? itemTypeId = null,
            Guid? vatId = null,
            Guid? uomGroupId = null,
            Guid? inventoryUOMId = null,
            Guid? purUOMId = null,
            Guid? salesUOMId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr5Id = null,
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
            Guid? attr19Id = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Item.Code.Contains(filterText) || e.Item.Name.Contains(filterText) || e.Item.ShortName.Contains(filterText) || e.Item.erpCode.Contains(filterText) || e.Item.Barcode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Item.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Item.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(shortName), e => e.Item.ShortName.Contains(shortName))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.Item.erpCode.Contains(erpCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(barcode), e => e.Item.Barcode.Contains(barcode))
                    .WhereIf(isPurchasable.HasValue, e => e.Item.IsPurchasable == isPurchasable)
                    .WhereIf(isSaleable.HasValue, e => e.Item.IsSaleable == isSaleable)
                    .WhereIf(isInventoriable.HasValue, e => e.Item.IsInventoriable == isInventoriable)
                    .WhereIf(basePriceMin.HasValue, e => e.Item.BasePrice >= basePriceMin.Value)
                    .WhereIf(basePriceMax.HasValue, e => e.Item.BasePrice <= basePriceMax.Value)
                    .WhereIf(active.HasValue, e => e.Item.Active == active)
                    .WhereIf(manageItemBy.HasValue, e => e.Item.ManageItemBy == manageItemBy)
                    .WhereIf(expiredType.HasValue, e => e.Item.ExpiredType == expiredType)
                    .WhereIf(expiredValueMin.HasValue, e => e.Item.ExpiredValue >= expiredValueMin.Value)
                    .WhereIf(expiredValueMax.HasValue, e => e.Item.ExpiredValue <= expiredValueMax.Value)
                    .WhereIf(issueMethod.HasValue, e => e.Item.IssueMethod == issueMethod)
                    .WhereIf(canUpdate.HasValue, e => e.Item.CanUpdate == canUpdate)
                    .WhereIf(purUnitRateMin.HasValue, e => e.Item.PurUnitRate >= purUnitRateMin.Value)
                    .WhereIf(purUnitRateMax.HasValue, e => e.Item.PurUnitRate <= purUnitRateMax.Value)
                    .WhereIf(salesUnitRateMin.HasValue, e => e.Item.SalesUnitRate >= salesUnitRateMin.Value)
                    .WhereIf(salesUnitRateMax.HasValue, e => e.Item.SalesUnitRate <= salesUnitRateMax.Value)
                    .WhereIf(itemTypeId != null && itemTypeId != Guid.Empty, e => e.SystemData != null && e.SystemData.Id == itemTypeId)
                    .WhereIf(vatId != null && vatId != Guid.Empty, e => e.VAT != null && e.VAT.Id == vatId)
                    .WhereIf(uomGroupId != null && uomGroupId != Guid.Empty, e => e.UOMGroup != null && e.UOMGroup.Id == uomGroupId)
                    .WhereIf(inventoryUOMId != null && inventoryUOMId != Guid.Empty, e => e.InventoryUnit != null && e.InventoryUnit.Id == inventoryUOMId)
                    .WhereIf(purUOMId != null && purUOMId != Guid.Empty, e => e.PurUnit != null && e.PurUnit.Id == purUOMId)
                    .WhereIf(salesUOMId != null && salesUOMId != Guid.Empty, e => e.SalesUnit != null && e.SalesUnit.Id == salesUOMId)
                    .WhereIf(attr0Id != null && attr0Id != Guid.Empty, e => e.Attr0 != null && e.Attr0.Id == attr0Id)
                    .WhereIf(attr1Id != null && attr1Id != Guid.Empty, e => e.Attr1 != null && e.Attr1.Id == attr1Id)
                    .WhereIf(attr2Id != null && attr2Id != Guid.Empty, e => e.Attr2 != null && e.Attr2.Id == attr2Id)
                    .WhereIf(attr3Id != null && attr3Id != Guid.Empty, e => e.Attr3 != null && e.Attr3.Id == attr3Id)
                    .WhereIf(attr4Id != null && attr4Id != Guid.Empty, e => e.Attr4 != null && e.Attr4.Id == attr4Id)
                    .WhereIf(attr5Id != null && attr5Id != Guid.Empty, e => e.Attr5 != null && e.Attr5.Id == attr5Id)
                    .WhereIf(attr6Id != null && attr6Id != Guid.Empty, e => e.Attr6 != null && e.Attr6.Id == attr6Id)
                    .WhereIf(attr7Id != null && attr7Id != Guid.Empty, e => e.Attr7 != null && e.Attr7.Id == attr7Id)
                    .WhereIf(attr8Id != null && attr8Id != Guid.Empty, e => e.Attr8 != null && e.Attr8.Id == attr8Id)
                    .WhereIf(attr9Id != null && attr9Id != Guid.Empty, e => e.Attr9 != null && e.Attr9.Id == attr9Id)
                    .WhereIf(attr10Id != null && attr10Id != Guid.Empty, e => e.Attr10 != null && e.Attr10.Id == attr10Id)
                    .WhereIf(attr11Id != null && attr11Id != Guid.Empty, e => e.Attr11 != null && e.Attr11.Id == attr11Id)
                    .WhereIf(attr12Id != null && attr12Id != Guid.Empty, e => e.Attr12 != null && e.Attr12.Id == attr12Id)
                    .WhereIf(attr13Id != null && attr13Id != Guid.Empty, e => e.Attr13 != null && e.Attr13.Id == attr13Id)
                    .WhereIf(attr14Id != null && attr14Id != Guid.Empty, e => e.Attr14 != null && e.Attr14.Id == attr14Id)
                    .WhereIf(attr15Id != null && attr15Id != Guid.Empty, e => e.Attr15 != null && e.Attr15.Id == attr15Id)
                    .WhereIf(attr16Id != null && attr16Id != Guid.Empty, e => e.Attr16 != null && e.Attr16.Id == attr16Id)
                    .WhereIf(attr17Id != null && attr17Id != Guid.Empty, e => e.Attr17 != null && e.Attr17.Id == attr17Id)
                    .WhereIf(attr18Id != null && attr18Id != Guid.Empty, e => e.Attr18 != null && e.Attr18.Id == attr18Id)
                    .WhereIf(attr19Id != null && attr19Id != Guid.Empty, e => e.Attr19 != null && e.Attr19.Id == attr19Id);
        }

        public async Task<List<Item>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string erpCode = null,
            string barcode = null,
            bool? isPurchasable = null,
            bool? isSaleable = null,
            bool? isInventoriable = null,
            decimal? basePriceMin = null,
            decimal? basePriceMax = null,
            bool? active = null,
            ManageBy? manageItemBy = null,
            ExpiredType? expiredType = null,
            int? expiredValueMin = null,
            int? expiredValueMax = null,
            IssueMethod? issueMethod = null,
            bool? canUpdate = null,
            decimal? purUnitRateMin = null,
            decimal? purUnitRateMax = null,
            decimal? salesUnitRateMin = null,
            decimal? salesUnitRateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, shortName, erpCode, barcode, isPurchasable, isSaleable, isInventoriable, basePriceMin, basePriceMax, active, manageItemBy, expiredType, expiredValueMin, expiredValueMax, issueMethod, canUpdate, purUnitRateMin, purUnitRateMax, salesUnitRateMin, salesUnitRateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string erpCode = null,
            string barcode = null,
            bool? isPurchasable = null,
            bool? isSaleable = null,
            bool? isInventoriable = null,
            decimal? basePriceMin = null,
            decimal? basePriceMax = null,
            bool? active = null,
            ManageBy? manageItemBy = null,
            ExpiredType? expiredType = null,
            int? expiredValueMin = null,
            int? expiredValueMax = null,
            IssueMethod? issueMethod = null,
            bool? canUpdate = null,
            decimal? purUnitRateMin = null,
            decimal? purUnitRateMax = null,
            decimal? salesUnitRateMin = null,
            decimal? salesUnitRateMax = null,
            Guid? itemTypeId = null,
            Guid? vatId = null,
            Guid? uomGroupId = null,
            Guid? inventoryUOMId = null,
            Guid? purUOMId = null,
            Guid? salesUOMId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr5Id = null,
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, shortName, erpCode, barcode, isPurchasable, isSaleable, isInventoriable, basePriceMin, basePriceMax, active, manageItemBy, expiredType, expiredValueMin, expiredValueMax, issueMethod, canUpdate, purUnitRateMin, purUnitRateMax, salesUnitRateMin, salesUnitRateMax, itemTypeId, vatId, uomGroupId, inventoryUOMId, purUOMId, salesUOMId, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Item> ApplyFilter(
            IQueryable<Item> query,
            string filterText,
            string code = null,
            string name = null,
            string shortName = null,
            string erpCode = null,
            string barcode = null,
            bool? isPurchasable = null,
            bool? isSaleable = null,
            bool? isInventoriable = null,
            decimal? basePriceMin = null,
            decimal? basePriceMax = null,
            bool? active = null,
            ManageBy? manageItemBy = null,
            ExpiredType? expiredType = null,
            int? expiredValueMin = null,
            int? expiredValueMax = null,
            IssueMethod? issueMethod = null,
            bool? canUpdate = null,
            decimal? purUnitRateMin = null,
            decimal? purUnitRateMax = null,
            decimal? salesUnitRateMin = null,
            decimal? salesUnitRateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText) || e.ShortName.Contains(filterText) || e.erpCode.Contains(filterText) || e.Barcode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(shortName), e => e.ShortName.Contains(shortName))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.erpCode.Contains(erpCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(barcode), e => e.Barcode.Contains(barcode))
                    .WhereIf(isPurchasable.HasValue, e => e.IsPurchasable == isPurchasable)
                    .WhereIf(isSaleable.HasValue, e => e.IsSaleable == isSaleable)
                    .WhereIf(isInventoriable.HasValue, e => e.IsInventoriable == isInventoriable)
                    .WhereIf(basePriceMin.HasValue, e => e.BasePrice >= basePriceMin.Value)
                    .WhereIf(basePriceMax.HasValue, e => e.BasePrice <= basePriceMax.Value)
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(manageItemBy.HasValue, e => e.ManageItemBy == manageItemBy)
                    .WhereIf(expiredType.HasValue, e => e.ExpiredType == expiredType)
                    .WhereIf(expiredValueMin.HasValue, e => e.ExpiredValue >= expiredValueMin.Value)
                    .WhereIf(expiredValueMax.HasValue, e => e.ExpiredValue <= expiredValueMax.Value)
                    .WhereIf(issueMethod.HasValue, e => e.IssueMethod == issueMethod)
                    .WhereIf(canUpdate.HasValue, e => e.CanUpdate == canUpdate)
                    .WhereIf(purUnitRateMin.HasValue, e => e.PurUnitRate >= purUnitRateMin.Value)
                    .WhereIf(purUnitRateMax.HasValue, e => e.PurUnitRate <= purUnitRateMax.Value)
                    .WhereIf(salesUnitRateMin.HasValue, e => e.SalesUnitRate >= salesUnitRateMin.Value)
                    .WhereIf(salesUnitRateMax.HasValue, e => e.SalesUnitRate <= salesUnitRateMax.Value);
        }
    }
}
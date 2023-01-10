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
    public class EfCoreItemRepository : EfCoreRepository<MdmServiceDbContext, Item, Guid>, IItemRepository
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
                    UOMGroupDetail = dbContext.UOMGroupDetails.FirstOrDefault(c => c.Id == item.InventoryUOMId),
                    UOMGroupDetail1 = dbContext.UOMGroupDetails.FirstOrDefault(c => c.Id == item.PurUOMId),
                    UOMGroupDetail2 = dbContext.UOMGroupDetails.FirstOrDefault(c => c.Id == item.SalesUOMId),
                    ItemAttributeValue = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr0Id),
                    ItemAttributeValue1 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr1Id),
                    ItemAttributeValue2 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr2Id),
                    ItemAttributeValue3 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr3Id),
                    ItemAttributeValue4 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr4Id),
                    ItemAttributeValue5 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr5Id),
                    ItemAttributeValue6 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr6Id),
                    ItemAttributeValue7 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr7Id),
                    ItemAttributeValue8 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr8Id),
                    ItemAttributeValue9 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr9Id),
                    ItemAttributeValue10 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr10Id),
                    ItemAttributeValue11 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr11Id),
                    ItemAttributeValue12 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr12Id),
                    ItemAttributeValue13 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr13Id),
                    ItemAttributeValue14 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr14Id),
                    ItemAttributeValue15 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr15Id),
                    ItemAttributeValue16 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr16Id),
                    ItemAttributeValue17 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr17Id),
                    ItemAttributeValue18 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr18Id),
                    ItemAttributeValue19 = dbContext.ItemAttributeValues.FirstOrDefault(c => c.Id == item.Attr19Id)
                }).FirstOrDefault();
        }

        public async Task<List<ItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string eRPCode = null,
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
            query = ApplyFilter(query, filterText, code, name, shortName, eRPCode, barcode, isPurchasable, isSaleable, isInventoriable, basePriceMin, basePriceMax, active, manageItemBy, expiredType, expiredValueMin, expiredValueMax, issueMethod, canUpdate, itemTypeId, vatId, uomGroupId, inventoryUOMId, purUOMId, salesUOMId, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id);
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
                   join uOMGroupDetail in (await GetDbContextAsync()).UOMGroupDetails on item.InventoryUOMId equals uOMGroupDetail.Id into uOMGroupDetails
                   from uOMGroupDetail in uOMGroupDetails.DefaultIfEmpty()
                   join uOMGroupDetail1 in (await GetDbContextAsync()).UOMGroupDetails on item.PurUOMId equals uOMGroupDetail1.Id into uOMGroupDetails1
                   from uOMGroupDetail1 in uOMGroupDetails1.DefaultIfEmpty()
                   join uOMGroupDetail2 in (await GetDbContextAsync()).UOMGroupDetails on item.SalesUOMId equals uOMGroupDetail2.Id into uOMGroupDetails2
                   from uOMGroupDetail2 in uOMGroupDetails2.DefaultIfEmpty()
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
                       UOMGroupDetail = uOMGroupDetail,
                       UOMGroupDetail1 = uOMGroupDetail1,
                       UOMGroupDetail2 = uOMGroupDetail2,
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

        protected virtual IQueryable<ItemWithNavigationProperties> ApplyFilter(
            IQueryable<ItemWithNavigationProperties> query,
            string filterText,
            string code = null,
            string name = null,
            string shortName = null,
            string eRPCode = null,
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
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Item.Code.Contains(filterText) || e.Item.Name.Contains(filterText) || e.Item.ShortName.Contains(filterText) || e.Item.ERPCode.Contains(filterText) || e.Item.Barcode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Item.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Item.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(shortName), e => e.Item.ShortName.Contains(shortName))
                    .WhereIf(!string.IsNullOrWhiteSpace(eRPCode), e => e.Item.ERPCode.Contains(eRPCode))
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
                    .WhereIf(itemTypeId != null && itemTypeId != Guid.Empty, e => e.SystemData != null && e.SystemData.Id == itemTypeId)
                    .WhereIf(vatId != null && vatId != Guid.Empty, e => e.VAT != null && e.VAT.Id == vatId)
                    .WhereIf(uomGroupId != null && uomGroupId != Guid.Empty, e => e.UOMGroup != null && e.UOMGroup.Id == uomGroupId)
                    .WhereIf(inventoryUOMId != null && inventoryUOMId != Guid.Empty, e => e.UOMGroupDetail != null && e.UOMGroupDetail.Id == inventoryUOMId)
                    .WhereIf(purUOMId != null && purUOMId != Guid.Empty, e => e.UOMGroupDetail1 != null && e.UOMGroupDetail1.Id == purUOMId)
                    .WhereIf(salesUOMId != null && salesUOMId != Guid.Empty, e => e.UOMGroupDetail2 != null && e.UOMGroupDetail2.Id == salesUOMId)
                    .WhereIf(attr0Id != null && attr0Id != Guid.Empty, e => e.ItemAttributeValue != null && e.ItemAttributeValue.Id == attr0Id)
                    .WhereIf(attr1Id != null && attr1Id != Guid.Empty, e => e.ItemAttributeValue1 != null && e.ItemAttributeValue1.Id == attr1Id)
                    .WhereIf(attr2Id != null && attr2Id != Guid.Empty, e => e.ItemAttributeValue2 != null && e.ItemAttributeValue2.Id == attr2Id)
                    .WhereIf(attr3Id != null && attr3Id != Guid.Empty, e => e.ItemAttributeValue3 != null && e.ItemAttributeValue3.Id == attr3Id)
                    .WhereIf(attr4Id != null && attr4Id != Guid.Empty, e => e.ItemAttributeValue4 != null && e.ItemAttributeValue4.Id == attr4Id)
                    .WhereIf(attr5Id != null && attr5Id != Guid.Empty, e => e.ItemAttributeValue5 != null && e.ItemAttributeValue5.Id == attr5Id)
                    .WhereIf(attr6Id != null && attr6Id != Guid.Empty, e => e.ItemAttributeValue6 != null && e.ItemAttributeValue6.Id == attr6Id)
                    .WhereIf(attr7Id != null && attr7Id != Guid.Empty, e => e.ItemAttributeValue7 != null && e.ItemAttributeValue7.Id == attr7Id)
                    .WhereIf(attr8Id != null && attr8Id != Guid.Empty, e => e.ItemAttributeValue8 != null && e.ItemAttributeValue8.Id == attr8Id)
                    .WhereIf(attr9Id != null && attr9Id != Guid.Empty, e => e.ItemAttributeValue9 != null && e.ItemAttributeValue9.Id == attr9Id)
                    .WhereIf(attr10Id != null && attr10Id != Guid.Empty, e => e.ItemAttributeValue10 != null && e.ItemAttributeValue10.Id == attr10Id)
                    .WhereIf(attr11Id != null && attr11Id != Guid.Empty, e => e.ItemAttributeValue11 != null && e.ItemAttributeValue11.Id == attr11Id)
                    .WhereIf(attr12Id != null && attr12Id != Guid.Empty, e => e.ItemAttributeValue12 != null && e.ItemAttributeValue12.Id == attr12Id)
                    .WhereIf(attr13Id != null && attr13Id != Guid.Empty, e => e.ItemAttributeValue13 != null && e.ItemAttributeValue13.Id == attr13Id)
                    .WhereIf(attr14Id != null && attr14Id != Guid.Empty, e => e.ItemAttributeValue14 != null && e.ItemAttributeValue14.Id == attr14Id)
                    .WhereIf(attr15Id != null && attr15Id != Guid.Empty, e => e.ItemAttributeValue15 != null && e.ItemAttributeValue15.Id == attr15Id)
                    .WhereIf(attr16Id != null && attr16Id != Guid.Empty, e => e.ItemAttributeValue16 != null && e.ItemAttributeValue16.Id == attr16Id)
                    .WhereIf(attr17Id != null && attr17Id != Guid.Empty, e => e.ItemAttributeValue17 != null && e.ItemAttributeValue17.Id == attr17Id)
                    .WhereIf(attr18Id != null && attr18Id != Guid.Empty, e => e.ItemAttributeValue18 != null && e.ItemAttributeValue18.Id == attr18Id)
                    .WhereIf(attr19Id != null && attr19Id != Guid.Empty, e => e.ItemAttributeValue19 != null && e.ItemAttributeValue19.Id == attr19Id);
        }

        public async Task<List<Item>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string eRPCode = null,
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
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, shortName, eRPCode, barcode, isPurchasable, isSaleable, isInventoriable, basePriceMin, basePriceMax, active, manageItemBy, expiredType, expiredValueMin, expiredValueMax, issueMethod, canUpdate);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string eRPCode = null,
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
            query = ApplyFilter(query, filterText, code, name, shortName, eRPCode, barcode, isPurchasable, isSaleable, isInventoriable, basePriceMin, basePriceMax, active, manageItemBy, expiredType, expiredValueMin, expiredValueMax, issueMethod, canUpdate, itemTypeId, vatId, uomGroupId, inventoryUOMId, purUOMId, salesUOMId, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Item> ApplyFilter(
            IQueryable<Item> query,
            string filterText,
            string code = null,
            string name = null,
            string shortName = null,
            string eRPCode = null,
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
            bool? canUpdate = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText) || e.ShortName.Contains(filterText) || e.ERPCode.Contains(filterText) || e.Barcode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(shortName), e => e.ShortName.Contains(shortName))
                    .WhereIf(!string.IsNullOrWhiteSpace(eRPCode), e => e.ERPCode.Contains(eRPCode))
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
                    .WhereIf(canUpdate.HasValue, e => e.CanUpdate == canUpdate);
        }
    }
}
using DMSpro.OMS.MdmService.ItemMasters;
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

namespace DMSpro.OMS.MdmService.ItemMasters
{
    public class EfCoreItemMasterRepository : EfCoreRepository<MdmServiceDbContext, ItemMaster, Guid>, IItemMasterRepository
    {
        public EfCoreItemMasterRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ItemMasterWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(itemMaster => new ItemMasterWithNavigationProperties
                {
                    ItemMaster = itemMaster,
                    SystemData = dbContext.SystemDatas.FirstOrDefault(c => c.Id == itemMaster.ItemTypeId),
                    VAT = dbContext.VATs.FirstOrDefault(c => c.Id == itemMaster.VATId),
                    UOMGroup = dbContext.UOMGroups.FirstOrDefault(c => c.Id == itemMaster.UOMGroupId),
                    UOM = dbContext.UOMs.FirstOrDefault(c => c.Id == itemMaster.InventoryUnitId),
                    UOM1 = dbContext.UOMs.FirstOrDefault(c => c.Id == itemMaster.PurUnitId),
                    UOM2 = dbContext.UOMs.FirstOrDefault(c => c.Id == itemMaster.SalesUnit),
                    ProdAttributeValue = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr0Id),
                    ProdAttributeValue1 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr1Id),
                    ProdAttributeValue2 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr2Id),
                    ProdAttributeValue3 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr3Id),
                    ProdAttributeValue4 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr4Id),
                    ProdAttributeValue5 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr5Id),
                    ProdAttributeValue6 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr6Id),
                    ProdAttributeValue7 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr7Id),
                    ProdAttributeValue8 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr8Id),
                    ProdAttributeValue9 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr9Id),
                    ProdAttributeValue10 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr10Id),
                    ProdAttributeValue11 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr11Id),
                    ProdAttributeValue12 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr12Id),
                    ProdAttributeValue13 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr13Id),
                    ProdAttributeValue14 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr14Id),
                    ProdAttributeValue15 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr15Id),
                    ProdAttributeValue16 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr16Id),
                    ProdAttributeValue17 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr17Id),
                    ProdAttributeValue18 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr18Id),
                    ProdAttributeValue19 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == itemMaster.Attr19Id)
                }).FirstOrDefault();
        }

        public async Task<List<ItemMasterWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string erpCode = null,
            string barcode = null,
            bool? purchasble = null,
            bool? saleable = null,
            bool? inventoriable = null,
            bool? active = null,
            ManageType? manageType = null,
            ExpiredType? expiredType = null,
            int? expiredValueMin = null,
            int? expiredValueMax = null,
            IssueMethod? issueMethod = null,
            bool? canUpdate = null,
            int? basePriceMin = null,
            int? basePriceMax = null,
            Guid? itemTypeId = null,
            Guid? vATId = null,
            Guid? uOMGroupId = null,
            Guid? inventoryUnitId = null,
            Guid? purUnitId = null,
            Guid? salesUnit = null,
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
            query = ApplyFilter(query, filterText, code, name, shortName, erpCode, barcode, purchasble, saleable, inventoriable, active, manageType, expiredType, expiredValueMin, expiredValueMax, issueMethod, canUpdate, basePriceMin, basePriceMax, itemTypeId, vATId, uOMGroupId, inventoryUnitId, purUnitId, salesUnit, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemMasterConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ItemMasterWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from itemMaster in (await GetDbSetAsync())
                   join systemData in (await GetDbContextAsync()).SystemDatas on itemMaster.ItemTypeId equals systemData.Id into systemDatas
                   from systemData in systemDatas.DefaultIfEmpty()
                   join vAT in (await GetDbContextAsync()).VATs on itemMaster.VATId equals vAT.Id into vATs
                   from vAT in vATs.DefaultIfEmpty()
                   join uOMGroup in (await GetDbContextAsync()).UOMGroups on itemMaster.UOMGroupId equals uOMGroup.Id into uOMGroups
                   from uOMGroup in uOMGroups.DefaultIfEmpty()
                   join uOM in (await GetDbContextAsync()).UOMs on itemMaster.InventoryUnitId equals uOM.Id into uOMs
                   from uOM in uOMs.DefaultIfEmpty()
                   join uOM1 in (await GetDbContextAsync()).UOMs on itemMaster.PurUnitId equals uOM1.Id into uOMs1
                   from uOM1 in uOMs1.DefaultIfEmpty()
                   join uOM2 in (await GetDbContextAsync()).UOMs on itemMaster.SalesUnit equals uOM2.Id into uOMs2
                   from uOM2 in uOMs2.DefaultIfEmpty()
                   join prodAttributeValue in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr0Id equals prodAttributeValue.Id into prodAttributeValues
                   from prodAttributeValue in prodAttributeValues.DefaultIfEmpty()
                   join prodAttributeValue1 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr1Id equals prodAttributeValue1.Id into prodAttributeValues1
                   from prodAttributeValue1 in prodAttributeValues1.DefaultIfEmpty()
                   join prodAttributeValue2 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr2Id equals prodAttributeValue2.Id into prodAttributeValues2
                   from prodAttributeValue2 in prodAttributeValues2.DefaultIfEmpty()
                   join prodAttributeValue3 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr3Id equals prodAttributeValue3.Id into prodAttributeValues3
                   from prodAttributeValue3 in prodAttributeValues3.DefaultIfEmpty()
                   join prodAttributeValue4 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr4Id equals prodAttributeValue4.Id into prodAttributeValues4
                   from prodAttributeValue4 in prodAttributeValues4.DefaultIfEmpty()
                   join prodAttributeValue5 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr5Id equals prodAttributeValue5.Id into prodAttributeValues5
                   from prodAttributeValue5 in prodAttributeValues5.DefaultIfEmpty()
                   join prodAttributeValue6 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr6Id equals prodAttributeValue6.Id into prodAttributeValues6
                   from prodAttributeValue6 in prodAttributeValues6.DefaultIfEmpty()
                   join prodAttributeValue7 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr7Id equals prodAttributeValue7.Id into prodAttributeValues7
                   from prodAttributeValue7 in prodAttributeValues7.DefaultIfEmpty()
                   join prodAttributeValue8 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr8Id equals prodAttributeValue8.Id into prodAttributeValues8
                   from prodAttributeValue8 in prodAttributeValues8.DefaultIfEmpty()
                   join prodAttributeValue9 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr9Id equals prodAttributeValue9.Id into prodAttributeValues9
                   from prodAttributeValue9 in prodAttributeValues9.DefaultIfEmpty()
                   join prodAttributeValue10 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr10Id equals prodAttributeValue10.Id into prodAttributeValues10
                   from prodAttributeValue10 in prodAttributeValues10.DefaultIfEmpty()
                   join prodAttributeValue11 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr11Id equals prodAttributeValue11.Id into prodAttributeValues11
                   from prodAttributeValue11 in prodAttributeValues11.DefaultIfEmpty()
                   join prodAttributeValue12 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr12Id equals prodAttributeValue12.Id into prodAttributeValues12
                   from prodAttributeValue12 in prodAttributeValues12.DefaultIfEmpty()
                   join prodAttributeValue13 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr13Id equals prodAttributeValue13.Id into prodAttributeValues13
                   from prodAttributeValue13 in prodAttributeValues13.DefaultIfEmpty()
                   join prodAttributeValue14 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr14Id equals prodAttributeValue14.Id into prodAttributeValues14
                   from prodAttributeValue14 in prodAttributeValues14.DefaultIfEmpty()
                   join prodAttributeValue15 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr15Id equals prodAttributeValue15.Id into prodAttributeValues15
                   from prodAttributeValue15 in prodAttributeValues15.DefaultIfEmpty()
                   join prodAttributeValue16 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr16Id equals prodAttributeValue16.Id into prodAttributeValues16
                   from prodAttributeValue16 in prodAttributeValues16.DefaultIfEmpty()
                   join prodAttributeValue17 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr17Id equals prodAttributeValue17.Id into prodAttributeValues17
                   from prodAttributeValue17 in prodAttributeValues17.DefaultIfEmpty()
                   join prodAttributeValue18 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr18Id equals prodAttributeValue18.Id into prodAttributeValues18
                   from prodAttributeValue18 in prodAttributeValues18.DefaultIfEmpty()
                   join prodAttributeValue19 in (await GetDbContextAsync()).ProdAttributeValues on itemMaster.Attr19Id equals prodAttributeValue19.Id into prodAttributeValues19
                   from prodAttributeValue19 in prodAttributeValues19.DefaultIfEmpty()

                   select new ItemMasterWithNavigationProperties
                   {
                       ItemMaster = itemMaster,
                       SystemData = systemData,
                       VAT = vAT,
                       UOMGroup = uOMGroup,
                       UOM = uOM,
                       UOM1 = uOM1,
                       UOM2 = uOM2,
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

        protected virtual IQueryable<ItemMasterWithNavigationProperties> ApplyFilter(
            IQueryable<ItemMasterWithNavigationProperties> query,
            string filterText,
            string code = null,
            string name = null,
            string shortName = null,
            string erpCode = null,
            string barcode = null,
            bool? purchasble = null,
            bool? saleable = null,
            bool? inventoriable = null,
            bool? active = null,
            ManageType? manageType = null,
            ExpiredType? expiredType = null,
            int? expiredValueMin = null,
            int? expiredValueMax = null,
            IssueMethod? issueMethod = null,
            bool? canUpdate = null,
            int? basePriceMin = null,
            int? basePriceMax = null,
            Guid? itemTypeId = null,
            Guid? vATId = null,
            Guid? uOMGroupId = null,
            Guid? inventoryUnitId = null,
            Guid? purUnitId = null,
            Guid? salesUnit = null,
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
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ItemMaster.Code.Contains(filterText) || e.ItemMaster.Name.Contains(filterText) || e.ItemMaster.ShortName.Contains(filterText) || e.ItemMaster.ERPCode.Contains(filterText) || e.ItemMaster.Barcode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.ItemMaster.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.ItemMaster.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(shortName), e => e.ItemMaster.ShortName.Contains(shortName))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.ItemMaster.ERPCode.Contains(erpCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(barcode), e => e.ItemMaster.Barcode.Contains(barcode))
                    .WhereIf(purchasble.HasValue, e => e.ItemMaster.Purchasble == purchasble)
                    .WhereIf(saleable.HasValue, e => e.ItemMaster.Saleable == saleable)
                    .WhereIf(inventoriable.HasValue, e => e.ItemMaster.Inventoriable == inventoriable)
                    .WhereIf(active.HasValue, e => e.ItemMaster.Active == active)
                    .WhereIf(manageType.HasValue, e => e.ItemMaster.ManageType == manageType)
                    .WhereIf(expiredType.HasValue, e => e.ItemMaster.ExpiredType == expiredType)
                    .WhereIf(expiredValueMin.HasValue, e => e.ItemMaster.ExpiredValue >= expiredValueMin.Value)
                    .WhereIf(expiredValueMax.HasValue, e => e.ItemMaster.ExpiredValue <= expiredValueMax.Value)
                    .WhereIf(issueMethod.HasValue, e => e.ItemMaster.IssueMethod == issueMethod)
                    .WhereIf(canUpdate.HasValue, e => e.ItemMaster.CanUpdate == canUpdate)
                    .WhereIf(basePriceMin.HasValue, e => e.ItemMaster.BasePrice >= basePriceMin.Value)
                    .WhereIf(basePriceMax.HasValue, e => e.ItemMaster.BasePrice <= basePriceMax.Value)
                    .WhereIf(itemTypeId != null && itemTypeId != Guid.Empty, e => e.SystemData != null && e.SystemData.Id == itemTypeId)
                    .WhereIf(vATId != null && vATId != Guid.Empty, e => e.VAT != null && e.VAT.Id == vATId)
                    .WhereIf(uOMGroupId != null && uOMGroupId != Guid.Empty, e => e.UOMGroup != null && e.UOMGroup.Id == uOMGroupId)
                    .WhereIf(inventoryUnitId != null && inventoryUnitId != Guid.Empty, e => e.UOM != null && e.UOM.Id == inventoryUnitId)
                    .WhereIf(purUnitId != null && purUnitId != Guid.Empty, e => e.UOM1 != null && e.UOM1.Id == purUnitId)
                    .WhereIf(salesUnit != null && salesUnit != Guid.Empty, e => e.UOM2 != null && e.UOM2.Id == salesUnit)
                    .WhereIf(attr0Id != null && attr0Id != Guid.Empty, e => e.ProdAttributeValue != null && e.ProdAttributeValue.Id == attr0Id)
                    .WhereIf(attr1Id != null && attr1Id != Guid.Empty, e => e.ProdAttributeValue1 != null && e.ProdAttributeValue1.Id == attr1Id)
                    .WhereIf(attr2Id != null && attr2Id != Guid.Empty, e => e.ProdAttributeValue2 != null && e.ProdAttributeValue2.Id == attr2Id)
                    .WhereIf(attr3Id != null && attr3Id != Guid.Empty, e => e.ProdAttributeValue3 != null && e.ProdAttributeValue3.Id == attr3Id)
                    .WhereIf(attr4Id != null && attr4Id != Guid.Empty, e => e.ProdAttributeValue4 != null && e.ProdAttributeValue4.Id == attr4Id)
                    .WhereIf(attr5Id != null && attr5Id != Guid.Empty, e => e.ProdAttributeValue5 != null && e.ProdAttributeValue5.Id == attr5Id)
                    .WhereIf(attr6Id != null && attr6Id != Guid.Empty, e => e.ProdAttributeValue6 != null && e.ProdAttributeValue6.Id == attr6Id)
                    .WhereIf(attr7Id != null && attr7Id != Guid.Empty, e => e.ProdAttributeValue7 != null && e.ProdAttributeValue7.Id == attr7Id)
                    .WhereIf(attr8Id != null && attr8Id != Guid.Empty, e => e.ProdAttributeValue8 != null && e.ProdAttributeValue8.Id == attr8Id)
                    .WhereIf(attr9Id != null && attr9Id != Guid.Empty, e => e.ProdAttributeValue9 != null && e.ProdAttributeValue9.Id == attr9Id)
                    .WhereIf(attr10Id != null && attr10Id != Guid.Empty, e => e.ProdAttributeValue10 != null && e.ProdAttributeValue10.Id == attr10Id)
                    .WhereIf(attr11Id != null && attr11Id != Guid.Empty, e => e.ProdAttributeValue11 != null && e.ProdAttributeValue11.Id == attr11Id)
                    .WhereIf(attr12Id != null && attr12Id != Guid.Empty, e => e.ProdAttributeValue12 != null && e.ProdAttributeValue12.Id == attr12Id)
                    .WhereIf(attr13Id != null && attr13Id != Guid.Empty, e => e.ProdAttributeValue13 != null && e.ProdAttributeValue13.Id == attr13Id)
                    .WhereIf(attr14Id != null && attr14Id != Guid.Empty, e => e.ProdAttributeValue14 != null && e.ProdAttributeValue14.Id == attr14Id)
                    .WhereIf(attr15Id != null && attr15Id != Guid.Empty, e => e.ProdAttributeValue15 != null && e.ProdAttributeValue15.Id == attr15Id)
                    .WhereIf(attr16Id != null && attr16Id != Guid.Empty, e => e.ProdAttributeValue16 != null && e.ProdAttributeValue16.Id == attr16Id)
                    .WhereIf(attr17Id != null && attr17Id != Guid.Empty, e => e.ProdAttributeValue17 != null && e.ProdAttributeValue17.Id == attr17Id)
                    .WhereIf(attr18Id != null && attr18Id != Guid.Empty, e => e.ProdAttributeValue18 != null && e.ProdAttributeValue18.Id == attr18Id)
                    .WhereIf(attr19Id != null && attr19Id != Guid.Empty, e => e.ProdAttributeValue19 != null && e.ProdAttributeValue19.Id == attr19Id);
        }

        public async Task<List<ItemMaster>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string erpCode = null,
            string barcode = null,
            bool? purchasble = null,
            bool? saleable = null,
            bool? inventoriable = null,
            bool? active = null,
            ManageType? manageType = null,
            ExpiredType? expiredType = null,
            int? expiredValueMin = null,
            int? expiredValueMax = null,
            IssueMethod? issueMethod = null,
            bool? canUpdate = null,
            int? basePriceMin = null,
            int? basePriceMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, shortName, erpCode, barcode, purchasble, saleable, inventoriable, active, manageType, expiredType, expiredValueMin, expiredValueMax, issueMethod, canUpdate, basePriceMin, basePriceMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemMasterConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string erpCode = null,
            string barcode = null,
            bool? purchasble = null,
            bool? saleable = null,
            bool? inventoriable = null,
            bool? active = null,
            ManageType? manageType = null,
            ExpiredType? expiredType = null,
            int? expiredValueMin = null,
            int? expiredValueMax = null,
            IssueMethod? issueMethod = null,
            bool? canUpdate = null,
            int? basePriceMin = null,
            int? basePriceMax = null,
            Guid? itemTypeId = null,
            Guid? vATId = null,
            Guid? uOMGroupId = null,
            Guid? inventoryUnitId = null,
            Guid? purUnitId = null,
            Guid? salesUnit = null,
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
            query = ApplyFilter(query, filterText, code, name, shortName, erpCode, barcode, purchasble, saleable, inventoriable, active, manageType, expiredType, expiredValueMin, expiredValueMax, issueMethod, canUpdate, basePriceMin, basePriceMax, itemTypeId, vATId, uOMGroupId, inventoryUnitId, purUnitId, salesUnit, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemMaster> ApplyFilter(
            IQueryable<ItemMaster> query,
            string filterText,
            string code = null,
            string name = null,
            string shortName = null,
            string erpCode = null,
            string barcode = null,
            bool? purchasble = null,
            bool? saleable = null,
            bool? inventoriable = null,
            bool? active = null,
            ManageType? manageType = null,
            ExpiredType? expiredType = null,
            int? expiredValueMin = null,
            int? expiredValueMax = null,
            IssueMethod? issueMethod = null,
            bool? canUpdate = null,
            int? basePriceMin = null,
            int? basePriceMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText) || e.ShortName.Contains(filterText) || e.ERPCode.Contains(filterText) || e.Barcode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(shortName), e => e.ShortName.Contains(shortName))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.ERPCode.Contains(erpCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(barcode), e => e.Barcode.Contains(barcode))
                    .WhereIf(purchasble.HasValue, e => e.Purchasble == purchasble)
                    .WhereIf(saleable.HasValue, e => e.Saleable == saleable)
                    .WhereIf(inventoriable.HasValue, e => e.Inventoriable == inventoriable)
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(manageType.HasValue, e => e.ManageType == manageType)
                    .WhereIf(expiredType.HasValue, e => e.ExpiredType == expiredType)
                    .WhereIf(expiredValueMin.HasValue, e => e.ExpiredValue >= expiredValueMin.Value)
                    .WhereIf(expiredValueMax.HasValue, e => e.ExpiredValue <= expiredValueMax.Value)
                    .WhereIf(issueMethod.HasValue, e => e.IssueMethod == issueMethod)
                    .WhereIf(canUpdate.HasValue, e => e.CanUpdate == canUpdate)
                    .WhereIf(basePriceMin.HasValue, e => e.BasePrice >= basePriceMin.Value)
                    .WhereIf(basePriceMax.HasValue, e => e.BasePrice <= basePriceMax.Value);
        }
    }
}
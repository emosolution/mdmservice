using DMSpro.OMS.MdmService.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemManager : DomainService
    {
        private readonly IItemRepository _itemRepository;

        public ItemManager(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Item> CreateAsync(
        Guid itemTypeId, Guid vatId, Guid uomGroupId, Guid inventoryUOMId, Guid purUOMId, Guid salesUOMId, Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, string code, string name, string shortName, string eRPCode, string barcode, bool isPurchasable, bool isSaleable, bool isInventoriable, decimal basePrice, bool active, ManageBy manageItemBy, bool canUpdate, ExpiredType? expiredType = null, int? expiredValue = null, IssueMethod? issueMethod = null)
        {
            Check.NotNull(itemTypeId, nameof(itemTypeId));
            Check.NotNull(vatId, nameof(vatId));
            Check.NotNull(uomGroupId, nameof(uomGroupId));
            Check.NotNull(inventoryUOMId, nameof(inventoryUOMId));
            Check.NotNull(purUOMId, nameof(purUOMId));
            Check.NotNull(salesUOMId, nameof(salesUOMId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), ItemConsts.CodeMaxLength, ItemConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), ItemConsts.NameMaxLength, ItemConsts.NameMinLength);
            Check.Length(shortName, nameof(shortName), ItemConsts.ShortNameMaxLength);
            Check.Length(eRPCode, nameof(eRPCode), ItemConsts.ERPCodeMaxLength);
            Check.Length(barcode, nameof(barcode), ItemConsts.BarcodeMaxLength);
            Check.NotNull(manageItemBy, nameof(manageItemBy));

            var item = new Item(
             GuidGenerator.Create(),
             itemTypeId, vatId, uomGroupId, inventoryUOMId, purUOMId, salesUOMId, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id, code, name, shortName, eRPCode, barcode, isPurchasable, isSaleable, isInventoriable, basePrice, active, manageItemBy, canUpdate, expiredType, expiredValue, issueMethod
             );

            return await _itemRepository.InsertAsync(item);
        }

        public async Task<Item> UpdateAsync(
            Guid id,
            Guid itemTypeId, Guid vatId, Guid uomGroupId, Guid inventoryUOMId, Guid purUOMId, Guid salesUOMId, Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, string code, string name, string shortName, string eRPCode, string barcode, bool isPurchasable, bool isSaleable, bool isInventoriable, decimal basePrice, bool active, ManageBy manageItemBy, bool canUpdate, ExpiredType? expiredType = null, int? expiredValue = null, IssueMethod? issueMethod = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemTypeId, nameof(itemTypeId));
            Check.NotNull(vatId, nameof(vatId));
            Check.NotNull(uomGroupId, nameof(uomGroupId));
            Check.NotNull(inventoryUOMId, nameof(inventoryUOMId));
            Check.NotNull(purUOMId, nameof(purUOMId));
            Check.NotNull(salesUOMId, nameof(salesUOMId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), ItemConsts.CodeMaxLength, ItemConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), ItemConsts.NameMaxLength, ItemConsts.NameMinLength);
            Check.Length(shortName, nameof(shortName), ItemConsts.ShortNameMaxLength);
            Check.Length(eRPCode, nameof(eRPCode), ItemConsts.ERPCodeMaxLength);
            Check.Length(barcode, nameof(barcode), ItemConsts.BarcodeMaxLength);
            Check.NotNull(manageItemBy, nameof(manageItemBy));

            var queryable = await _itemRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var item = await AsyncExecuter.FirstOrDefaultAsync(query);

            item.ItemTypeId = itemTypeId;
            item.VatId = vatId;
            item.UomGroupId = uomGroupId;
            item.InventoryUOMId = inventoryUOMId;
            item.PurUOMId = purUOMId;
            item.SalesUOMId = salesUOMId;
            item.Attr0Id = attr0Id;
            item.Attr1Id = attr1Id;
            item.Attr2Id = attr2Id;
            item.Attr3Id = attr3Id;
            item.Attr4Id = attr4Id;
            item.Attr5Id = attr5Id;
            item.Attr6Id = attr6Id;
            item.Attr7Id = attr7Id;
            item.Attr8Id = attr8Id;
            item.Attr9Id = attr9Id;
            item.Attr10Id = attr10Id;
            item.Attr11Id = attr11Id;
            item.Attr12Id = attr12Id;
            item.Attr13Id = attr13Id;
            item.Attr14Id = attr14Id;
            item.Attr15Id = attr15Id;
            item.Attr16Id = attr16Id;
            item.Attr17Id = attr17Id;
            item.Attr18Id = attr18Id;
            item.Attr19Id = attr19Id;
            item.Code = code;
            item.Name = name;
            item.ShortName = shortName;
            item.ERPCode = eRPCode;
            item.Barcode = barcode;
            item.IsPurchasable = isPurchasable;
            item.IsSaleable = isSaleable;
            item.IsInventoriable = isInventoriable;
            item.BasePrice = basePrice;
            item.Active = active;
            item.ManageItemBy = manageItemBy;
            item.CanUpdate = canUpdate;
            item.ExpiredType = expiredType;
            item.ExpiredValue = expiredValue;
            item.IssueMethod = issueMethod;

            item.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemRepository.UpdateAsync(item);
        }

    }
}
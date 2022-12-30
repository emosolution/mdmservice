using DMSpro.OMS.MdmService.ItemMasters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemMasters
{
    public class ItemMasterManager : DomainService
    {
        private readonly IItemMasterRepository _itemMasterRepository;

        public ItemMasterManager(IItemMasterRepository itemMasterRepository)
        {
            _itemMasterRepository = itemMasterRepository;
        }

        public async Task<ItemMaster> CreateAsync(
        Guid itemTypeId, Guid vATId, Guid uOMGroupId, Guid inventoryUnitId, Guid purUnitId, Guid salesUnit, Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, string code, string name, string shortName, string erpCode, string barcode, bool purchasble, bool saleable, bool inventoriable, bool active, ManageType manageType, ExpiredType expiredType, int expiredValue, IssueMethod issueMethod, bool canUpdate, int basePrice)
        {
            Check.NotNull(itemTypeId, nameof(itemTypeId));
            Check.NotNull(vATId, nameof(vATId));
            Check.NotNull(uOMGroupId, nameof(uOMGroupId));
            Check.NotNull(inventoryUnitId, nameof(inventoryUnitId));
            Check.NotNull(purUnitId, nameof(purUnitId));
            Check.NotNull(salesUnit, nameof(salesUnit));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), ItemMasterConsts.CodeMaxLength, ItemMasterConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), ItemMasterConsts.NameMaxLength, ItemMasterConsts.NameMinLength);
            Check.Length(shortName, nameof(shortName), ItemMasterConsts.ShortNameMaxLength, ItemMasterConsts.ShortNameMinLength);
            Check.Length(erpCode, nameof(erpCode), ItemMasterConsts.ERPCodeMaxLength, ItemMasterConsts.ERPCodeMinLength);
            Check.Length(barcode, nameof(barcode), ItemMasterConsts.BarcodeMaxLength, ItemMasterConsts.BarcodeMinLength);
            Check.NotNull(manageType, nameof(manageType));
            Check.NotNull(expiredType, nameof(expiredType));
            Check.NotNull(issueMethod, nameof(issueMethod));

            var itemMaster = new ItemMaster(
             GuidGenerator.Create(),
             itemTypeId, vATId, uOMGroupId, inventoryUnitId, purUnitId, salesUnit, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id, code, name, shortName, erpCode, barcode, purchasble, saleable, inventoriable, active, manageType, expiredType, expiredValue, issueMethod, canUpdate, basePrice
             );

            return await _itemMasterRepository.InsertAsync(itemMaster);
        }

        public async Task<ItemMaster> UpdateAsync(
            Guid id,
            Guid itemTypeId, Guid vATId, Guid uOMGroupId, Guid inventoryUnitId, Guid purUnitId, Guid salesUnit, Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, string code, string name, string shortName, string erpCode, string barcode, bool purchasble, bool saleable, bool inventoriable, bool active, ManageType manageType, ExpiredType expiredType, int expiredValue, IssueMethod issueMethod, bool canUpdate, int basePrice, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemTypeId, nameof(itemTypeId));
            Check.NotNull(vATId, nameof(vATId));
            Check.NotNull(uOMGroupId, nameof(uOMGroupId));
            Check.NotNull(inventoryUnitId, nameof(inventoryUnitId));
            Check.NotNull(purUnitId, nameof(purUnitId));
            Check.NotNull(salesUnit, nameof(salesUnit));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), ItemMasterConsts.CodeMaxLength, ItemMasterConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), ItemMasterConsts.NameMaxLength, ItemMasterConsts.NameMinLength);
            Check.Length(shortName, nameof(shortName), ItemMasterConsts.ShortNameMaxLength, ItemMasterConsts.ShortNameMinLength);
            Check.Length(erpCode, nameof(erpCode), ItemMasterConsts.ERPCodeMaxLength, ItemMasterConsts.ERPCodeMinLength);
            Check.Length(barcode, nameof(barcode), ItemMasterConsts.BarcodeMaxLength, ItemMasterConsts.BarcodeMinLength);
            Check.NotNull(manageType, nameof(manageType));
            Check.NotNull(expiredType, nameof(expiredType));
            Check.NotNull(issueMethod, nameof(issueMethod));

            var queryable = await _itemMasterRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var itemMaster = await AsyncExecuter.FirstOrDefaultAsync(query);

            itemMaster.ItemTypeId = itemTypeId;
            itemMaster.VATId = vATId;
            itemMaster.UOMGroupId = uOMGroupId;
            itemMaster.InventoryUnitId = inventoryUnitId;
            itemMaster.PurUnitId = purUnitId;
            itemMaster.SalesUnit = salesUnit;
            itemMaster.Attr0Id = attr0Id;
            itemMaster.Attr1Id = attr1Id;
            itemMaster.Attr2Id = attr2Id;
            itemMaster.Attr3Id = attr3Id;
            itemMaster.Attr4Id = attr4Id;
            itemMaster.Attr5Id = attr5Id;
            itemMaster.Attr6Id = attr6Id;
            itemMaster.Attr7Id = attr7Id;
            itemMaster.Attr8Id = attr8Id;
            itemMaster.Attr9Id = attr9Id;
            itemMaster.Attr10Id = attr10Id;
            itemMaster.Attr11Id = attr11Id;
            itemMaster.Attr12Id = attr12Id;
            itemMaster.Attr13Id = attr13Id;
            itemMaster.Attr14Id = attr14Id;
            itemMaster.Attr15Id = attr15Id;
            itemMaster.Attr16Id = attr16Id;
            itemMaster.Attr17Id = attr17Id;
            itemMaster.Attr18Id = attr18Id;
            itemMaster.Attr19Id = attr19Id;
            itemMaster.Code = code;
            itemMaster.Name = name;
            itemMaster.ShortName = shortName;
            itemMaster.ERPCode = erpCode;
            itemMaster.Barcode = barcode;
            itemMaster.Purchasble = purchasble;
            itemMaster.Saleable = saleable;
            itemMaster.Inventoriable = inventoriable;
            itemMaster.Active = active;
            itemMaster.ManageType = manageType;
            itemMaster.ExpiredType = expiredType;
            itemMaster.ExpiredValue = expiredValue;
            itemMaster.IssueMethod = issueMethod;
            itemMaster.CanUpdate = canUpdate;
            itemMaster.BasePrice = basePrice;

            itemMaster.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemMasterRepository.UpdateAsync(itemMaster);
        }

    }
}
using DMSpro.OMS.MdmService.ItemMasters;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.ProdAttributeValues;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemMasters
{
    public class ItemMaster : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string ShortName { get; set; }

        [CanBeNull]
        public virtual string ERPCode { get; set; }

        [CanBeNull]
        public virtual string Barcode { get; set; }

        public virtual bool Purchasble { get; set; }

        public virtual bool Saleable { get; set; }

        public virtual bool Inventoriable { get; set; }

        public virtual bool Active { get; set; }

        public virtual ManageType ManageType { get; set; }

        public virtual ExpiredType ExpiredType { get; set; }

        public virtual int ExpiredValue { get; set; }

        public virtual IssueMethod IssueMethod { get; set; }

        public virtual bool CanUpdate { get; set; }

        public virtual int BasePrice { get; set; }
        public Guid ItemTypeId { get; set; }
        public Guid VATId { get; set; }
        public Guid UOMGroupId { get; set; }
        public Guid InventoryUnitId { get; set; }
        public Guid PurUnitId { get; set; }
        public Guid SalesUnit { get; set; }
        public Guid? Attr0Id { get; set; }
        public Guid? Attr1Id { get; set; }
        public Guid? Attr2Id { get; set; }
        public Guid? Attr3Id { get; set; }
        public Guid? Attr4Id { get; set; }
        public Guid? Attr5Id { get; set; }
        public Guid? Attr6Id { get; set; }
        public Guid? Attr7Id { get; set; }
        public Guid? Attr8Id { get; set; }
        public Guid? Attr9Id { get; set; }
        public Guid? Attr10Id { get; set; }
        public Guid? Attr11Id { get; set; }
        public Guid? Attr12Id { get; set; }
        public Guid? Attr13Id { get; set; }
        public Guid? Attr14Id { get; set; }
        public Guid? Attr15Id { get; set; }
        public Guid? Attr16Id { get; set; }
        public Guid? Attr17Id { get; set; }
        public Guid? Attr18Id { get; set; }
        public Guid? Attr19Id { get; set; }

        public ItemMaster()
        {

        }

        public ItemMaster(Guid id, Guid itemTypeId, Guid vATId, Guid uOMGroupId, Guid inventoryUnitId, Guid purUnitId, Guid salesUnit, Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, string code, string name, string shortName, string erpCode, string barcode, bool purchasble, bool saleable, bool inventoriable, bool active, ManageType manageType, ExpiredType expiredType, int expiredValue, IssueMethod issueMethod, bool canUpdate, int basePrice)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), ItemMasterConsts.CodeMaxLength, ItemMasterConsts.CodeMinLength);
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), ItemMasterConsts.NameMaxLength, ItemMasterConsts.NameMinLength);
            Check.Length(shortName, nameof(shortName), ItemMasterConsts.ShortNameMaxLength, ItemMasterConsts.ShortNameMinLength);
            Check.Length(erpCode, nameof(erpCode), ItemMasterConsts.ERPCodeMaxLength, ItemMasterConsts.ERPCodeMinLength);
            Check.Length(barcode, nameof(barcode), ItemMasterConsts.BarcodeMaxLength, ItemMasterConsts.BarcodeMinLength);
            Code = code;
            Name = name;
            ShortName = shortName;
            ERPCode = erpCode;
            Barcode = barcode;
            Purchasble = purchasble;
            Saleable = saleable;
            Inventoriable = inventoriable;
            Active = active;
            ManageType = manageType;
            ExpiredType = expiredType;
            ExpiredValue = expiredValue;
            IssueMethod = issueMethod;
            CanUpdate = canUpdate;
            BasePrice = basePrice;
            ItemTypeId = itemTypeId;
            VATId = vATId;
            UOMGroupId = uOMGroupId;
            InventoryUnitId = inventoryUnitId;
            PurUnitId = purUnitId;
            SalesUnit = salesUnit;
            Attr0Id = attr0Id;
            Attr1Id = attr1Id;
            Attr2Id = attr2Id;
            Attr3Id = attr3Id;
            Attr4Id = attr4Id;
            Attr5Id = attr5Id;
            Attr6Id = attr6Id;
            Attr7Id = attr7Id;
            Attr8Id = attr8Id;
            Attr9Id = attr9Id;
            Attr10Id = attr10Id;
            Attr11Id = attr11Id;
            Attr12Id = attr12Id;
            Attr13Id = attr13Id;
            Attr14Id = attr14Id;
            Attr15Id = attr15Id;
            Attr16Id = attr16Id;
            Attr17Id = attr17Id;
            Attr18Id = attr18Id;
            Attr19Id = attr19Id;
        }

    }
}
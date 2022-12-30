using DMSpro.OMS.MdmService.ItemMasters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemMasters
{
    public class ItemMasterCreateDto
    {
        [Required]
        [StringLength(ItemMasterConsts.CodeMaxLength, MinimumLength = ItemMasterConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(ItemMasterConsts.NameMaxLength, MinimumLength = ItemMasterConsts.NameMinLength)]
        public string Name { get; set; }
        [StringLength(ItemMasterConsts.ShortNameMaxLength, MinimumLength = ItemMasterConsts.ShortNameMinLength)]
        public string ShortName { get; set; }
        [StringLength(ItemMasterConsts.ERPCodeMaxLength, MinimumLength = ItemMasterConsts.ERPCodeMinLength)]
        public string ERPCode { get; set; }
        [StringLength(ItemMasterConsts.BarcodeMaxLength, MinimumLength = ItemMasterConsts.BarcodeMinLength)]
        public string Barcode { get; set; }
        public bool Purchasble { get; set; } = true;
        public bool Saleable { get; set; } = true;
        public bool Inventoriable { get; set; } = true;
        public bool Active { get; set; } = true;
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ManageType ManageType { get; set; } = ((ManageType[])Enum.GetValues(typeof(ManageType)))[0];
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ExpiredType ExpiredType { get; set; } = ((ExpiredType[])Enum.GetValues(typeof(ExpiredType)))[0];
        public int ExpiredValue { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public IssueMethod IssueMethod { get; set; } = ((IssueMethod[])Enum.GetValues(typeof(IssueMethod)))[0];
        public bool CanUpdate { get; set; } = true;
        public int BasePrice { get; set; } = 0;
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
    }
}
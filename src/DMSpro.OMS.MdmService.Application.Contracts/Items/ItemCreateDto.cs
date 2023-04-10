using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemCreateDto
    {
        [Required]
        [StringLength(ItemConsts.NameMaxLength, MinimumLength = ItemConsts.NameMinLength)]
        public string Name { get; set; }
        [StringLength(ItemConsts.ShortNameMaxLength)]
        public string ShortName { get; set; }
        [StringLength(ItemConsts.erpCodeMaxLength)]
        public string erpCode { get; set; }
        [StringLength(ItemConsts.BarcodeMaxLength)]
        public string Barcode { get; set; }
        public bool IsPurchasable { get; set; } = true;
        public bool IsSaleable { get; set; } = true;
        public bool IsInventoriable { get; set; } = true;
        public decimal BasePrice { get; set; }
        public bool Active { get; set; } = true;
        public ManageBy ManageItemBy { get; set; } = ((ManageBy[])Enum.GetValues(typeof(ManageBy)))[0];
        public ExpiredType? ExpiredType { get; set; }
        public int? ExpiredValue { get; set; }
        public IssueMethod? IssueMethod { get; set; }
        public bool CanUpdate { get; set; } = true;
        public decimal PurUnitRate { get; set; }
        public decimal SalesUnitRate { get; set; }
        public ItemTypes ItemType { get; set; } = ((ItemTypes[])Enum.GetValues(typeof(ItemTypes)))[0];
        public Guid VatId { get; set; }
        public Guid UomGroupId { get; set; }
        public Guid InventoryUOMId { get; set; }
        public Guid PurUOMId { get; set; }
        public Guid SalesUOMId { get; set; }
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
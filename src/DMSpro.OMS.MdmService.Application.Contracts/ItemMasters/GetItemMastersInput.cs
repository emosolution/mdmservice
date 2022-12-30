using DMSpro.OMS.MdmService.ItemMasters;
using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ItemMasters
{
    public class GetItemMastersInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string ERPCode { get; set; }
        public string Barcode { get; set; }
        public bool? Purchasble { get; set; }
        public bool? Saleable { get; set; }
        public bool? Inventoriable { get; set; }
        public bool? Active { get; set; }
        public ManageType? ManageType { get; set; }
        public ExpiredType? ExpiredType { get; set; }
        public int? ExpiredValueMin { get; set; }
        public int? ExpiredValueMax { get; set; }
        public IssueMethod? IssueMethod { get; set; }
        public bool? CanUpdate { get; set; }
        public int? BasePriceMin { get; set; }
        public int? BasePriceMax { get; set; }
        public Guid? ItemTypeId { get; set; }
        public Guid? VATId { get; set; }
        public Guid? UOMGroupId { get; set; }
        public Guid? InventoryUnitId { get; set; }
        public Guid? PurUnitId { get; set; }
        public Guid? SalesUnit { get; set; }
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

        public GetItemMastersInput()
        {

        }
    }
}
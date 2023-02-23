using DMSpro.OMS.MdmService.Items;
using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.Items
{
    public class GetItemsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string erpCode { get; set; }
        public string Barcode { get; set; }
        public bool? IsPurchasable { get; set; }
        public bool? IsSaleable { get; set; }
        public bool? IsInventoriable { get; set; }
        public decimal? BasePriceMin { get; set; }
        public decimal? BasePriceMax { get; set; }
        public bool? Active { get; set; }
        public ManageBy? ManageItemBy { get; set; }
        public ExpiredType? ExpiredType { get; set; }
        public int? ExpiredValueMin { get; set; }
        public int? ExpiredValueMax { get; set; }
        public IssueMethod? IssueMethod { get; set; }
        public bool? CanUpdate { get; set; }
        public decimal? PurUnitRateMin { get; set; }
        public decimal? PurUnitRateMax { get; set; }
        public decimal? SalesUnitRateMin { get; set; }
        public decimal? SalesUnitRateMax { get; set; }
        public Guid? ItemTypeId { get; set; }
        public Guid? VatId { get; set; }
        public Guid? UomGroupId { get; set; }
        public Guid? InventoryUOMId { get; set; }
        public Guid? PurUOMId { get; set; }
        public Guid? SalesUOMId { get; set; }
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

        public GetItemsInput()
        {

        }
    }
}
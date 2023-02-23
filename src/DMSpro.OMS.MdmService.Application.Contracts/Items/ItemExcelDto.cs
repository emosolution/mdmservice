using DMSpro.OMS.MdmService.Items;
using System;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemExcelDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string erpCode { get; set; }
        public string Barcode { get; set; }
        public bool IsPurchasable { get; set; }
        public bool IsSaleable { get; set; }
        public bool IsInventoriable { get; set; }
        public decimal BasePrice { get; set; }
        public bool Active { get; set; }
        public ManageBy ManageItemBy { get; set; }
        public ExpiredType? ExpiredType { get; set; }
        public int? ExpiredValue { get; set; }
        public IssueMethod? IssueMethod { get; set; }
        public bool CanUpdate { get; set; }
        public decimal PurUnitRate { get; set; }
        public decimal SalesUnitRate { get; set; }
    }
}
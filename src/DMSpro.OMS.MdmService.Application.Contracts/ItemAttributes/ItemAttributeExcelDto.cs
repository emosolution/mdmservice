using System;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributeExcelDto
    {
        public string AttrNo { get; set; }
        public string AttrName { get; set; }
        public int? HierarchyLevel { get; set; }
        public bool Active { get; set; }
        public bool IsSellingCategory { get; set; }
    }
}
using System;

namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public class ProductAttributeExcelDto
    {
        public int AttrNo { get; set; }
        public string AttrName { get; set; }
        public int? HierarchyLevel { get; set; }
        public bool Active { get; set; }
        public bool IsProductCategory { get; set; }
    }
}
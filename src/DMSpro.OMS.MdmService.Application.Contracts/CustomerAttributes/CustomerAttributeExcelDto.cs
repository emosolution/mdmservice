using System;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributeExcelDto
    {
        public int AttrNo { get; set; }
        public string AttrName { get; set; }
        public int? HierarchyLevel { get; set; }
        public bool Active { get; set; }
    }
}
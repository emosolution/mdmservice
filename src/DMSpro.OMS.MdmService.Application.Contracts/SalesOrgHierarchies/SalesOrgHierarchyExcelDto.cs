using System;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyExcelDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsRoute { get; set; }
        public bool IsSellingZone { get; set; }
        public string HierarchyCode { get; set; }
        public bool Active { get; set; }
    }
}
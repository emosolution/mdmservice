using System;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeaderExcelDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
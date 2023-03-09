using System;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigExcelDto
    {
        public int StartNumber { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int Length { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
}
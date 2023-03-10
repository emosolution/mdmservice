using System;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigExcelDto
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int PaddingZeroNumber { get; set; }
        public string Description { get; set; }
    }
}
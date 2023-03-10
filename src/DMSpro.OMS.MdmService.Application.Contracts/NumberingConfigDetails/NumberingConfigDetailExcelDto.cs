using System;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailExcelDto
    {
        public string Description { get; set; }
        public string Prefix { get; set; }
        public int PaddingZeroNumber { get; set; }
        public string Suffix { get; set; }
        public bool Active { get; set; }
        public int CurrentNumber { get; set; }
    }
}
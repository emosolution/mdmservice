using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public static partial class NumberingConfigConsts
    {
        public const string SystemDataCode = "SY01";
        public static Dictionary<string, string> ObjectTypeDictionary = new()
        {
            {"Company", "M0" },
            {"Customer", "M1" },
            {"Employee", "M2" },
            {"SalesOrgHierarchy", "M3" },
            {"SalesRequest", "S0" },
        };

        public static Dictionary<string, string> PrefixDictionary = new()
        {
            {"SalesRequest", "SR" },
        };

        public static Dictionary<string, int> PaddingZeroNumberDictionary = new()
        {
            {"SalesRequest", 5 },
        };

        public static Dictionary<string, string> SuffixDictionary = new() { };

        public const string DefaultCommonPrefix = "";
        public const int DefaultCommonPaddingZeroNumber = 5;
        public const string DefaultCommonSuffix = "";
    }
}
using System.Collections.Generic;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public static partial class NumberingConfigConsts
    {
        public const short PaddingZeroNumberMinValue = 0;
        public const short PaddingZeroNumberMaxValue = 10;

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


        public static (string, int, string) GetPresetDataOfConfig(string objectType)
        {
            string prefix = DefaultCommonPrefix;
            if (PrefixDictionary.TryGetValue(objectType,
                out string dictionaryPrefix))
            {
                prefix = dictionaryPrefix;
                ;
            }
            int paddingZeroNumber = DefaultCommonPaddingZeroNumber;
            if (PaddingZeroNumberDictionary.TryGetValue(objectType,
                out int dictionaryPaddingZeroNumber))
            {
                paddingZeroNumber = dictionaryPaddingZeroNumber;
            }
            string suffix = DefaultCommonSuffix;
            if (SuffixDictionary.TryGetValue(objectType,
                out string dictionarySuffix))
            {
                suffix = dictionarySuffix;
            }
            return (prefix, paddingZeroNumber, suffix);
        }

        public static (string, int, string) GetBaseData(string inputPrefix,
            int? inputPaddingZeroNumber, string inputSuffix, string objectType)
        {
            (string prefix, int paddingZeroNumber, string suffix) =
               GetPresetDataOfConfig(objectType);
            inputPrefix ??= prefix;
            inputPaddingZeroNumber = inputPaddingZeroNumber == null ? paddingZeroNumber : inputPaddingZeroNumber;
            inputSuffix ??= suffix;

            Check.Length(inputPrefix, nameof(inputPrefix), PrefixMaxLength);
            Check.Length(inputSuffix, nameof(inputSuffix), SuffixMaxLength);
            Check.Range((short)inputPaddingZeroNumber, nameof(inputPaddingZeroNumber),
                PaddingZeroNumberMinValue, PaddingZeroNumberMaxValue);
            return (prefix, paddingZeroNumber, suffix);
        }
    }
}
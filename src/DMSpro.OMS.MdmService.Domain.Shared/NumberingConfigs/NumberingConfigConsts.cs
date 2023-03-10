using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public static partial class NumberingConfigConsts
    {
        private const string DefaultSorting = "{0}Prefix asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "NumberingConfig." : string.Empty);
        }

        public const int PrefixMaxLength = 20;
        public const int SuffixMaxLength = 20;
        public const int DescriptionMaxLength = 255;
        public const short PaddingZeroNumberMinValue = 0;
    }
}
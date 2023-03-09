namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public static class NumberingConfigDetailConsts
    {
        private const string DefaultSorting = "{0}Active asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "NumberingConfigDetail." : string.Empty);
        }

        public const int DescriptionMaxLength = 255;
    }
}
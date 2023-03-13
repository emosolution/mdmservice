namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public static partial class NumberingConfigDetailConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "NumberingConfigDetail." : string.Empty);
        }
    }
}
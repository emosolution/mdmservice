namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public static class PriceUpdateDetailConsts
    {
        private const string DefaultSorting = "{0}PriceBeforeUpdate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PriceUpdateDetail." : string.Empty);
        }

    }
}
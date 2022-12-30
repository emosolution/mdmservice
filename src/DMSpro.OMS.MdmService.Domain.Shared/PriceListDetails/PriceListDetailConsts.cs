namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public static class PriceListDetailConsts
    {
        private const string DefaultSorting = "{0}Price asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PriceListDetail." : string.Empty);
        }

    }
}
namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public static class PriceUpdateConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PriceUpdate." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int DescriptionMaxLength = 500;
    }
}
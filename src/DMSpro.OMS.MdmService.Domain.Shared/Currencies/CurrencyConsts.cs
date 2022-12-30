namespace DMSpro.OMS.MdmService.Currencies
{
    public static class CurrencyConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Currency." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int NameMinLength = 0;
        public const int NameMaxLength = 100;
    }
}
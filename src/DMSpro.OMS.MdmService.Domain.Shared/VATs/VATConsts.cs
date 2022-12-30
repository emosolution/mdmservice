namespace DMSpro.OMS.MdmService.VATs
{
    public static class VATConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VAT." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int NameMinLength = 1;
        public const int NameMaxLength = 100;
        public const int RateMinLength = 0;
        public const int RateMaxLength = 99999;
    }
}
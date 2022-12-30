namespace DMSpro.OMS.MdmService.Vendors
{
    public static class VendorConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Vendor." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int NameMinLength = 1;
        public const int NameMaxLength = 200;
        public const int ShortNameMinLength = 1;
        public const int ShortNameMaxLength = 200;
    }
}
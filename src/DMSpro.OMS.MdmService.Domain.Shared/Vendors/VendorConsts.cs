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
        public const int Phone1MaxLength = 255;
        public const int Phone2MaxLength = 255;
        public const int ERPCodeMaxLength = 255;
        public const int StreetMaxLength = 255;
        public const int AddressMaxLength = 500;
        public const int LatitudeMaxLength = 255;
        public const int LongitudeMaxLength = 255;
    }
}
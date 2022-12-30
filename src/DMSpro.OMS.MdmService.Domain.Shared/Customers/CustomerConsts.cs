namespace DMSpro.OMS.MdmService.Customers
{
    public static class CustomerConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Customer." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int NameMaxLength = 255;
        public const int Phone1MaxLength = 50;
        public const int Phone2MaxLength = 50;
        public const int erpCodeMaxLength = 20;
        public const int LicenseMaxLength = 50;
        public const int TaxCodeMaxLength = 50;
        public const int vatNameMaxLength = 255;
        public const int vatAddressMaxLength = 1000;
        public const int StreetMaxLength = 255;
        public const int AddressMaxLength = 500;
        public const int LatitudeMaxLength = 255;
        public const int LongitudeMaxLength = 255;
        public const int SFACustomerCodeMinLength = 1;
        public const int SFACustomerCodeMaxLength = 20;
    }
}
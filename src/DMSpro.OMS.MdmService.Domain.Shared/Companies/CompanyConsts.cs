namespace DMSpro.OMS.MdmService.Companies
{
    public static class CompanyConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Company." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int NameMinLength = 1;
        public const int NameMaxLength = 100;
        public const int AddressMinLength = 1;
        public const int AddressMaxLength = 1000;
        public const int PhoneMinLength = 0;
        public const int PhoneMaxLength = 20;
        public const int LicenseMinLength = 0;
        public const int LicenseMaxLength = 100;
        public const int TaxCodeMaxLength = 100;
        public const int ERPCodeMaxLength = 100;
    }
}
namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public static class CustomerContactConsts
    {
        private const string DefaultSorting = "{0}Title asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerContact." : string.Empty);
        }

        public const int FirstNameMaxLength = 255;
        public const int LastNameMaxLength = 255;
        public const int PhoneMaxLength = 255;
        public const int EmailMaxLength = 255;
        public const int AddressMaxLength = 500;
        public const int IdentityNumberMaxLength = 255;
        public const int BankNameMaxLength = 255;
        public const int BankAccNameMaxLength = 255;
        public const int BankAccNumberMaxLength = 255;
    }
}